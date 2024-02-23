import { useEffect, useState, useMemo } from "react";
import { useNavigate, useParams } from "react-router-dom";
import useForm from "../../hooks/useForm";

import Figure from "react-bootstrap/Figure";
import Carousel from 'react-bootstrap/Carousel';
import Form from 'react-bootstrap/Form';

import * as productService from "../../services/productService";
import Path from "../../utils/paths";
import styles from "./ProductDetails.module.css";
import { OrderFormKeys } from "../../utils/constants";

export default function ProductDetails() {
    //Separate form in new component
    const { id } = useParams();
    const [product, setProduct] = useState({});
    const [errors, setErrors] = useState("");
    //const [isSubmitting, setIsSubmitting] = useState(false);
    const navigate = useNavigate();

    const errorMessages = {
        invalidSize: "No such size",
        notEnoughQuantity: "We don't have that many products",
        zeroOrEmptyInput: "Can't do this",
    }

    useEffect(() => {
        productService.getOne(id).then(setProduct).catch((err) => {
            console.log(err)
            navigate(Path.Items);
        });
    }, [id])

    const submitHandler = () => {
        const selectedSizeId = values.size;
        const selectedSize = product.sizes.find(size => size.id == selectedSizeId);
        if (!selectedSize) {
            setErrors(errorMessages.invalidSize);
            return;
        }

        // Access the quantity property of the selected size
        const availableQuantity = selectedSize.quantity;

        if (availableQuantity < parseInt(values.amount)) {
            setErrors(errorMessages.notEnoughQuantity)
            return;
        }

        if (parseInt(values.amount) <= 0 || values.amount == '') {
            setErrors(errorMessages.zeroOrEmptyInput)
            return;
        }

        setErrors("");
    }

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

    console.log(values)

    return (
        <div className={styles.container}>
            <div className={styles.content}>
                <Carousel fade className={styles.carousel} data-bs-theme="dark">

                    {Object.keys(product).length !== 0 &&
                        product.images.map((image, index) => {
                            const imgSrc = `data:image/jpeg;base64,${image.imageData}`;
                            return (
                                <Carousel.Item key={index}>
                                    <Figure.Image alt="product-img" src={imgSrc} className={styles.productImg} />
                                </Carousel.Item>
                            );
                        })}

                </Carousel>
                <div className={styles.productDetails}>
                    <h1 className={styles.productName}>{product.name}</h1>
                    <div className={styles.productInfo}>
                        <p>
                            <b>Price</b>: {product.price}
                        </p>
                        <p className={styles.description}>
                            <b>Description</b>: {product.description}
                        </p>
                        <Form onSubmit={onSubmit}>
                            {errors == errorMessages.invalidSize &&
                                (
                                    <p className={styles.invalid}>
                                        {errors}
                                    </p>
                                )}
                            <Form.Select name={OrderFormKeys.Size}
                                onChange={onChange}
                                value={values.size}>
                                <option value="">Select size</option>
                                {Object.keys(product).length !== 0 && product.sizes.filter(s => s.quantity > 0).map((size) => {
                                    return <option value={size.id} key={size.id} name={size.sizeChar}>
                                            {size.sizeChar}
                                           </option>
                                })}
                            </Form.Select>
                            <Form.Group className="mb-3">
                                {(errors === errorMessages.notEnoughQuantity || errors === errorMessages.zeroOrEmptyInput) &&
                                    (
                                        <p className={styles.invalid}>
                                            {errors}
                                        </p>
                                    )}
                                <Form.Control
                                    type="number"
                                    id={OrderFormKeys.Amount}
                                    name={OrderFormKeys.Amount}
                                    onChange={onChange}
                                    value={values.amount}
                                />
                            </Form.Group>
                            <button
                                className={styles.submitButton}
                                variant="primary"
                                type="submit"
                            >
                                Add to cart
                            </button>
                        </Form>
                    </div>
                </div>
            </div>
        </div>
    )
}