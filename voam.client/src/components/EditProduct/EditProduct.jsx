import { useFormik } from "formik";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState, useContext } from "react";

import { EditFormKeys } from "../../utils/constants";
import editProductValidation from "./editProductValidation";
import styles from "../../styles/FormStyles.module.css";
import Path from "../../utils/paths";
import * as imageToBinary from "../../utils/toBase64";
import * as productService from "../../services/productService";
import AuthContext from "../../context/authContext";

import Form from "react-bootstrap/Form";
import FloatingLabel from "react-bootstrap/FloatingLabel";

export default function EditProduct() {
  const navigate = useNavigate();
  const { productId } = useParams();
  const [inputKey, setInputKey] = useState(Date.now());
  const { logoutHandler } = useContext(AuthContext);

  const [product, setProduct] = useState({
    [EditFormKeys.Name]: "",
    [EditFormKeys.Description]: "",
    [EditFormKeys.Price]: "",
    [EditFormKeys.SizeS]: 0,
    [EditFormKeys.SizeM]: 0,
    [EditFormKeys.SizeL]: 0,
    [EditFormKeys.Images]: [],
  });

  useEffect(() => {
    productService.getOne(productId).then((result) => {
      setProduct(result);
      setInputKey(Date.now());
    });
  }, [productId]);

  const {
    values,
    errors,
    touched,
    handleBlur,
    handleChange,
    handleSubmit,
    isSubmitting,
    setFieldValue,
    setFieldError,
  } = useFormik({
    initialValues: {
      [EditFormKeys.Name]: product[EditFormKeys.Name] || "",
      [EditFormKeys.Description]: product[EditFormKeys.Description] || "",
      [EditFormKeys.Price]: product[EditFormKeys.Price] || "",
      [EditFormKeys.SizeS]:
        (
          product.sizes &&
          Array.isArray(product.sizes) &&
          product.sizes.find((s) => s.sizeChar === "S")
        )?.quantity || 0,
      [EditFormKeys.SizeM]:
        (
          product.sizes &&
          Array.isArray(product.sizes) &&
          product.sizes.find((s) => s.sizeChar === "M")
        )?.quantity || 0,
      [EditFormKeys.SizeL]:
        (
          product.sizes &&
          Array.isArray(product.sizes) &&
          product.sizes.find((s) => s.sizeChar === "L")
        )?.quantity || 0,
      [EditFormKeys.Images]: product[EditFormKeys.Images] || [],
    },
    validationSchema: editProductValidation,
    onSubmit,
    enableReinitialize: true,
  });

  async function onSubmit() {
    try {
      if (checkImagesLength()) {
        return;
      }

      if (!Array.isArray(values.images)) {
        let imagesArray = [];

        for (let i = 0; i < values.images.length; i++) {
          if (values.images[i].type !== "image/png") {
            setFieldError(EditFormKeys.Images, "Only PNG images are allowed");
            return;
          }

          let result = await imageToBinary.toBase64(values.images[i]);
          imagesArray.push(result);
        }

        values.images = imagesArray;
      } else {
        values.images = [];
      }

      await productService.edit(productId, values);

      navigate(`${Path.Items}/${productId}`);
    } catch (error) {
      logoutHandler();
      navigate(Path.Login);
      alert("Ooops... Something went wrong. Try login again");
    }
  }

  function checkImagesLength() {
    // If no image is found, return false
    if (values.images.length === 0) {
      setFieldError(EditFormKeys.Images, "Upload at least one image");
      return true;
    }

    return false;
  }

  return (
    <div className={styles.containerFormCreate}>
      <Form
        className={styles.formCreate}
        onSubmit={handleSubmit}
        encType="multipart/form-data"
      >
        <h1 className={styles.title}>Edit Product</h1>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.Name] && touched[EditFormKeys.Name] && (
            <p className={styles.invalid}>{errors[EditFormKeys.Name]}</p>
          )}
          <FloatingLabel
            htmlFor={EditFormKeys.Name}
            label="Product name"
            className="mb-3"
          >
            <Form.Control
              type="text"
              id="name"
              name={EditFormKeys.Name}
              placeholder="Enter product name"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.Name]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.Description] &&
            touched[EditFormKeys.Description] && (
              <p className={styles.invalid}>
                {errors[EditFormKeys.Description]}
              </p>
            )}
          <FloatingLabel
            htmlFor={EditFormKeys.Description}
            label="Product description"
            className="mb-3"
          >
            <Form.Control
              type="text"
              id="decsription"
              name={EditFormKeys.Description}
              placeholder="Enter product description"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.Description]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.Price] && touched[EditFormKeys.Price] && (
            <p className={styles.invalid}>{errors[EditFormKeys.Price]}</p>
          )}
          <FloatingLabel
            htmlFor={EditFormKeys.Price}
            label="Price"
            className="mb-3"
          >
            <Form.Control
              type="number"
              id="price"
              name={EditFormKeys.Price}
              placeholder="Enter product price"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.Price]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.SizeS] && touched[EditFormKeys.SizeS] && (
            <p className={styles.invalid}>{errors[EditFormKeys.SizeS]}</p>
          )}
          <FloatingLabel
            htmlFor={EditFormKeys.SizeS}
            label="S size amount"
            className="mb-3"
          >
            <Form.Control
              type="number"
              id={EditFormKeys.SizeS}
              name={EditFormKeys.SizeS}
              placeholder="Enter S size amount"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.SizeS]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.SizeM] && touched[EditFormKeys.SizeM] && (
            <p className={styles.invalid}>{errors[EditFormKeys.SizeM]}</p>
          )}
          <FloatingLabel
            htmlFor={EditFormKeys.SizeM}
            label="M size amount"
            className="mb-3"
          >
            <Form.Control
              type="number"
              id={EditFormKeys.SizeM}
              name={EditFormKeys.SizeM}
              placeholder="Enter S size amount"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.SizeM]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[EditFormKeys.SizeL] && touched[EditFormKeys.SizeL] && (
            <p className={styles.invalid}>{errors[EditFormKeys.SizeL]}</p>
          )}
          <FloatingLabel
            htmlFor={EditFormKeys.SizeL}
            label="L size amount"
            className="mb-3"
          >
            <Form.Control
              type="number"
              id={EditFormKeys.SizeL}
              name={EditFormKeys.SizeL}
              placeholder="Enter S size amount"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[EditFormKeys.SizeL]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group controlId="formFileMultiple" className="mb-3">
          {errors[EditFormKeys.Images] && touched[EditFormKeys.Images] && (
            <p className={styles.invalid}>{errors[EditFormKeys.Images]}</p>
          )}
          <label htmlFor={EditFormKeys.Images}></label>

          <input
            key={inputKey}
            multiple
            type="file"
            name={EditFormKeys.Images}
            id={EditFormKeys.Images}
            className={styles.fileInput}
            onChange={(e) => setFieldValue(EditFormKeys.Images, e.target.files)}
          />
        </Form.Group>

        <button
          className={styles.submitButton}
          type="submit"
          disabled={isSubmitting}
        >
          Edit
        </button>
      </Form>
    </div>
  );
}
