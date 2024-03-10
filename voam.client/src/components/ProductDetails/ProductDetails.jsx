import { useEffect, useState, useMemo, useContext } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import useForm from "../../hooks/useForm";

import Figure from "react-bootstrap/Figure";
import Carousel from "react-bootstrap/Carousel";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { FaStar } from "react-icons/fa6";

import * as productService from "../../services/productService";
import * as shoppingCartService from "../../services/shoppingCartService";
import AuthContext from "../../context/authContext.jsx";
import Path from "../../utils/paths";
import styles from "./ProductDetails.module.css";
import { OrderFormKeys } from "../../utils/constants";

export default function ProductDetails() {
  //Separate form in new component
  const { id } = useParams();
  const { isAuthenticated, userId } = useContext(AuthContext);
  const [product, setProduct] = useState({});
  const [errors, setErrors] = useState("");
  const navigate = useNavigate();
  const [rating, setRating] = useState(null);
  const [hover, setHover] = useState(null);

  //const [isSubmitting, setIsSubmitting] = useState(false);

  console.log(isAuthenticated)

  const errorMessages = {
    invalidSize: "Select product size",
    notEnoughQuantity: "We don't have that many products",
    zeroOrEmptyInput: "Can't do this",
  };

  useEffect(() => {
    productService
      .getOne(id)
      .then(setProduct)
      .catch((err) => {
        console.log(err);
        navigate(Path.Items);
      });
  }, [id]);

  const submitHandler = () => {
    const selectedSizeId = values.size;
    const selectedSize = product.sizes.find(
      (size) => size.id == selectedSizeId
    );
    if (!selectedSize) {
      setErrors(errorMessages.invalidSize);
      return;
    }

    // Access the quantity property of the selected size
    const availableQuantity = selectedSize.quantity;

    if (availableQuantity < parseInt(values.amount)) {
      setErrors(errorMessages.notEnoughQuantity);
      return;
    }

    if (parseInt(values.amount) <= 0 || values.amount == "") {
      setErrors(errorMessages.zeroOrEmptyInput);
      return;
    }
    setErrors("");

    const data = {
      userId: userId,
      productId: id,
      sizeId: values.size,
      quantity: values.amount
    }

    shoppingCartService.addToShoppingCart(data).catch((err) => {
      console.log(err)
    });
  };

  const deleteButtonClickHandler = async () => {
    const hasConfirmed = confirm(
      `Are you sure you want to delete ${product.name}`
    );

    if (hasConfirmed) {
      await productService.remove(id);

        navigate(Path.Items);
    }
  };

  const initialValues = useMemo(
    () => ({
      amount: 1,
      size: 0,
    }),
    []
  );

  const { values, onChange, onSubmit } = useForm(
    submitHandler,
    initialValues,
    product
  );

  return (
    <div className={styles.container}>
      <div className={styles.content}>
        <Carousel fade className={styles.carousel} data-bs-theme="dark">
          {Object.keys(product).length !== 0 &&
            product.images.map((image, index) => {
              const imgSrc = `data:image/jpeg;base64,${image.imageData}`;
              return (
                <Carousel.Item key={index}>
                  <Figure.Image
                    alt="product-img"
                    src={imgSrc}
                    className={styles.productImg}
                  />
                </Carousel.Item>
              );
            })}
        </Carousel>
        <div className={styles.productDetails}>
          <h1 className={styles.productName}>{product.name}</h1>
          <div className={styles.productInfo}>
            <p className={styles.price}>
              <b>Price</b>: {product.price} lv.
            </p>
            <p className={styles.description}>
              <b>Description</b>: {product.description}
            </p>
            <Form className={styles.formAddToCart} onSubmit={onSubmit}>
              <div className={styles.sizeAndQuantityContainer}>
                {errors == errorMessages.invalidSize && (
                  <p className={styles.invalid}>{errors}</p>
                )}
                <Form.Select
                  name={OrderFormKeys.Size}
                  onChange={onChange}
                  value={values.size}
                  className={styles.sizeSelector}
                >
                  <option value="">Select size</option>
                  {Object.keys(product).length !== 0 &&
                    product.sizes
                      .filter((s) => s.quantity > 0)
                      .map((size) => {
                        return (
                          <option
                            value={size.id}
                            key={size.id}
                            name={size.sizeChar}
                          >
                            {size.sizeChar}
                          </option>
                        );
                      })}
                </Form.Select>
                {(errors === errorMessages.notEnoughQuantity ||
                  errors === errorMessages.zeroOrEmptyInput) && (
                  <p className={styles.invalid}>{errors}</p>
                )}
                <Form.Control
                  type="number"
                  id={OrderFormKeys.Amount}
                  name={OrderFormKeys.Amount}
                  onChange={onChange}
                  value={values.amount}
                  className={styles.amountSelector}
                />
              </div>
              <Button
                className={styles.submitButton}
                type="submit"
                variant="success"
                disabled={!isAuthenticated}
              >
                {isAuthenticated ? "Add to cart" : "Log in to add to cart"}
              </Button>
            </Form>
            <div className={styles.ratingContainer}>
              <div className={styles.stars}>
                {[...Array(5)].map((star, index) => {
                  const currentRating = index + 1;
                  return (
                    <label key={index}>
                      <input
                        type="radio"
                        name="rating"
                        className={styles.ratingInput}
                        value={currentRating}
                        onClick={() => setRating(currentRating)}
                      />
                      <FaStar
                        className={styles.star}
                        size={30}
                        color={
                          currentRating <= (hover || rating)
                            ? "#ffc107"
                            : "#e4e5e9"
                        }
                        onMouseEnter={() => setHover(currentRating)}
                        onMouseLeave={() => setHover(null)}
                      />
                    </label>
                  );
                })}
              </div>
              <p className={styles.ratingNumber}>({rating})</p>
            </div>
          </div>
          <div className={styles.editAndDeleteContainer}>
            <Button
              as={Link}
              to={`/items/${id}/edit`}
              variant="warning"
              className={styles.editButton}
            >
              Edit
            </Button>
            <Button
              variant="danger"
              className={styles.deleteButton}
              onClick={deleteButtonClickHandler}
            >
              Delete
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}
