import { useEffect, useState } from "react";
import styles from "./RecentlyAddedProducts.module.css";
import RecentlyAddedProduct from "./RecentlyAddedProduct";
import * as productService from "../../../services/productService"


function RecentlyAddedProducts() {
    /*Fetch recently added products*/

    const [products, setProducts] = useState();

    useEffect(() => {
        productService.getLatest().then(res => setProducts(res))
    }, []);


    return (
        <div className={styles.recentProducts}>
            <h1 className={styles.title}>Recently added products:</h1>

            <div className={styles.containerProducts}>
                {products === undefined ? <p>Loading...</p> : products.map((product) => (
                    <RecentlyAddedProduct key={product.id} id={product.id} name={product.name} price={product.price} image={product.image.imageData} />
                ))}
            </div>
        </div>
    );
}

export default RecentlyAddedProducts;