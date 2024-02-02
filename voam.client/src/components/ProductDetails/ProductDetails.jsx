import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useFormik } from "formik";
import * as Yup from 'yup';

import Figure from "react-bootstrap/Figure";
import Carousel from 'react-bootstrap/Carousel';
import Form from 'react-bootstrap/Form';

import * as productService from "../../services/productService";
import Path from "../../utils/paths";
import styles from "./ProductDetails.module.css";
import { OrderFormKeys } from "../../utils/constants";

const initialValues = {
    [OrderFormKeys.Size]: 0,
    [OrderFormKeys.Amount]: 1,
}

export default function ProductDetails() {
    //Fix max amount value for every size 
    //Separate form in new component

    let maxValueAmount;



    const { id } = useParams();
    const [product, setProduct] = useState({});
    const navigate = useNavigate();




    useEffect(() => {
        productService.getOne(id).then(setProduct).catch((err) => {
            console.log(err)
            navigate(Path.Items);
        });
    }, [id])

    const validation = Yup.object({
        [OrderFormKeys.Amount]: Yup.number().integer().min(1).max(maxValueAmount).required()
    });

    const {
        values,
        handleChange,
        handleSubmit,
        isSubmitting,
        //resetForm,
    } = useFormik({
        initialValues,
        validationSchema: validation,
        onSubmit,
    });

    console.log(values.size)
    //maxValueAmount = 

    async function onSubmit() {

    }

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
                        <Form onSubmit={handleSubmit}>
                            <b>Size</b>:
                            <Form.Select name="sizeSelect" onChange={handleChange} value={values[OrderFormKeys.Size]}>
                                {Object.keys(product).length !== 0 && product.sizes.filter(s => s.quantity > 0).map((size) => {
                                    return <option value={size.id} key={size.id} name={size.sizeChar}>
                                            {size.sizeChar}
                                           </option>
                                })}
                            </Form.Select>
                            <Form.Group className="mb-3">
                                <Form.Control
                                    type="number"
                                    id="amount"
                                    name="amount"
                                    onChange={handleChange}
                                    value={values[OrderFormKeys.Amount]}
                                />
                            </Form.Group>
                            <button
                                className={styles.submitButton}
                                variant="primary"
                                type="submit"
                                disabled={isSubmitting}
                            >
                                Order
                            </button>
                        </Form>
                    </div>
                </div>
            </div>
        </div>
    )
}