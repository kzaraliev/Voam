import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";

import { EditFormKeys } from "../../utils/constants";
import registerValidation from "./createProductValidation";
import styles from "../../styles/FormStyles.module.css";
import Path from "../../utils/paths"
import * as imageToBinary from "../../utils/toBase64"
import * as productService from "../../services/productService"

import Form from "react-bootstrap/Form";
import FloatingLabel from "react-bootstrap/FloatingLabel";

const initialValues = {
    [EditFormKeys.Name]: "",
    [EditFormKeys.Description]: "",
    [EditFormKeys.Price]: "",
    [EditFormKeys.SizeS]: 0,
    [EditFormKeys.SizeM]: 0,
    [EditFormKeys.SizeL]: 0,
    [EditFormKeys.Images]: [],
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
            setFieldError(EditFormKeys.Images, 'Upload at least one image');
            return true;
        }

        return false;
    }

    return (
        <div className={styles.containerFormCreate}>
            <Form className={styles.formCreate} onSubmit={handleSubmit} encType="multipart/form-data">
                <h1 className={styles.title}>Create Product</h1>

                <Form.Group className="mb-3">
                    {errors[EditFormKeys.Name] &&
                        touched[EditFormKeys.Name] && (
                            <p className={styles.invalid}>
                                {errors[EditFormKeys.Name]}
                            </p>
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
                            <p className={styles.invalid}>{errors[EditFormKeys.Description]}</p>
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
                    {errors[EditFormKeys.Price] &&
                        touched[EditFormKeys.Price] && (
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
                    {errors[EditFormKeys.SizeS] &&
                        touched[EditFormKeys.SizeS] && (
                            <p className={styles.invalid}>{errors[EditFormKeys.SizeS]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={EditFormKeys.SizeS}
                        label="S size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeS"
                            name={EditFormKeys.SizeS}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[EditFormKeys.SizeS]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[EditFormKeys.SizeM] &&
                        touched[EditFormKeys.SizeM] && (
                            <p className={styles.invalid}>{errors[EditFormKeys.SizeM]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={EditFormKeys.SizeM}
                        label="M size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeM"
                            name={EditFormKeys.SizeM}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[EditFormKeys.SizeM]}
                        />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group className="mb-3">
                    {errors[EditFormKeys.SizeL] &&
                        touched[EditFormKeys.SizeL] && (
                            <p className={styles.invalid}>{errors[EditFormKeys.SizeL]}</p>
                        )}
                    <FloatingLabel
                        htmlFor={EditFormKeys.SizeL}
                        label="L size amount"
                        className="mb-3"
                    >
                        <Form.Control
                            type="number"
                            id="sizeL"
                            name={EditFormKeys.SizeL}
                            placeholder="Enter S size amount"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values[EditFormKeys.SizeL]}
                        />
                    </FloatingLabel>
                </Form.Group>



                <Form.Group controlId="formFileMultiple" className="mb-3">
                    {errors[EditFormKeys.Images] &&
                        touched[EditFormKeys.Images] && (
                        <p className={styles.invalid}>{errors[EditFormKeys.Images]}</p>
                        )}
                    <label htmlFor={EditFormKeys.Images}></label>

                    <input
                        multiple
                        type="file"
                        name={EditFormKeys.Images}
                        id={EditFormKeys.Images}
                        className={styles.fileInput}
                        onChange={(e) =>
                            setFieldValue(
                                EditFormKeys.Images,
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