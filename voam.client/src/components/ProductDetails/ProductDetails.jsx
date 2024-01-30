import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import Figure from "react-bootstrap/Figure";
import Carousel from 'react-bootstrap/Carousel';

import * as productService from "../../services/productService";
import Path from "../../utils/paths";
import styles from "./ProductDetails.module.css";

export default function ProductDetails() {

    const { id } = useParams();
    const [product, setProduct] = useState({});
    const navigate = useNavigate();


    useEffect(() => {
        productService.getOne(id).then(setProduct).catch((err) => {
            console.log(err)
            navigate(Path.Products);
        });
    }, [id])
    

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
                    </div>
                </div>
            </div>
        </div>
    )
}