import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";

import { CreateFormKeys } from "../../utils/constants";
import registerValidation from "./createProductValidation";
import styles from "../../styles/FormStyles.module.css";
import Path from "../../utils/paths"
import * as imageToBinary from "../../utils/toBase64"
import * as productService from "../../services/productService"

import Form from "react-bootstrap/Form";
import FloatingLabel from "react-bootstrap/FloatingLabel";

const initialValues = {
    [CreateFormKeys.Name]: "",
    [CreateFormKeys.Description]: "",
    [CreateFormKeys.Price]: "",
    [CreateFormKeys.IsAvailable]: false,
    [CreateFormKeys.SizeS]: 0,
    [CreateFormKeys.SizeM]: 0,
    [CreateFormKeys.SizeL]: 0,
    [CreateFormKeys.Images]: [],
};

export default function CreateProduct() {
    const navigate = useNavigate();

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
        resetForm,
    } = useFormik({
        initialValues,
        validationSchema: registerValidation,
        onSubmit,
    });


    async function onSubmit() {
        if (checkImagesLength()) {
            return;
        }

        let imagesArray = [];

        for (let i = 0; i < values.images.length; i++) {
            let result = await imageToBinary.toBase64(values.images[i]);
            imagesArray.push(result);
        }

        values.images = imagesArray;     

        console.log(values)
        try {
            const { id } = await productService.create(values);

            navigate(`${Path.Items}/${id}`);
        } catch (error) {
            if (error.code === 401) {
                resetForm();

                //logoutHandler();

                //navigate(Path.Login);
            }
        }
    }

    function checkImagesLength() {
        // If no image is found, return false
        if (values.images.length === 0) {
            setFieldError(CreateFormKeys.Images, 'Upload at least one image');
            return true;
        }

        return false;
    }

    return (
        <div className={styles.containerForm}>
            <Form className={styles.form} onSubmit={handleSubmit} encType="multipart/form-data">
                <h1 className={styles.title}>Create Product</h1>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.Name] &&
                        touched[CreateFormKeys.Name] && (
                            <p className={styles.invalid}>
                                {errors[CreateFormKeys.Name]}
                            </p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.Name}
                        label="Product name"
                        className="mb-3"
                    >
                        <Form.Control
                            type="text"
                            id="name"
                            name={CreateFormKeys.Name}
                            placeholder="Enter product name"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.Name]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.Description] &&
                        touched[CreateFormKeys.Description] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.Description]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.Description}
                        label="Product description"
                        className="mb-3"
                    >
                        <Form.Control
                            type="text"
                            id="decsription"
                            name={CreateFormKeys.Description}
                            placeholder="Enter product description"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.Description]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.Price] &&
                        touched[CreateFormKeys.Price] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.Price]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.Price}
                        label="Price"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="price"
                            name={CreateFormKeys.Price}
                            placeholder="Enter product price"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.Price]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.IsAvailable] &&
                        touched[CreateFormKeys.IsAvailable] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.IsAvailable]}</p>
                        )}
                    

                    <Form.Check // prettier-ignore
                        type="switch"
                        id="custom-switch"
                        name={CreateFormKeys.IsAvailable}
                        label={`Is this product available?`}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        value={values[CreateFormKeys.IsAvailable]}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.SizeS] &&
                        touched[CreateFormKeys.SizeS] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.SizeS]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.SizeS}
                        label="S size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeS"
                            name={CreateFormKeys.SizeS}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.SizeS]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.SizeM] &&
                        touched[CreateFormKeys.SizeM] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.SizeM]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.SizeM}
                        label="M size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeM"
                            name={CreateFormKeys.SizeM}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.SizeM]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[CreateFormKeys.SizeL] &&
                        touched[CreateFormKeys.SizeL] && (
                            <p className={styles.invalid}>{errors[CreateFormKeys.SizeL]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={CreateFormKeys.SizeL}
                        label="L size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeL"
                            name={CreateFormKeys.SizeL}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[CreateFormKeys.SizeL]}
                        />
                    </FloatingLabel>
                </Form.Group>



                <Form.Group controlId="formFileMultiple" className="mb-3">
                    {errors[CreateFormKeys.Images] &&
                        touched[CreateFormKeys.Images] && (
                        <p className={styles.invalid}>{errors[CreateFormKeys.Images]}</p>
                        )}
                    <label htmlFor={CreateFormKeys.Images}></label>

                    <input
                        multiple
                        type="file"
                        name={CreateFormKeys.Images}
                        id={CreateFormKeys.Images}
                        onChange={(e) =>
                            setFieldValue(
                                CreateFormKeys.Images,
                                e.target.files
                            )
                        }
                    />
                </Form.Group>

                <button
                    className={styles.submitButton}
                    variant="primary"
                    type="submit"
                    disabled={isSubmitting}
                >
                    Create
                </button>
            </Form>
        </div>
    );
}