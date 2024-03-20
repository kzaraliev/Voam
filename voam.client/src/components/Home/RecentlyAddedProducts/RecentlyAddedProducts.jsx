import { useEffect, useState } from "react";
import styles from "./RecentlyAddedProducts.module.css";
import RecentlyAddedProduct from "./RecentlyAddedProduct";
import * as productService from "../../../services/productService";
import Carousel from "react-bootstrap/Carousel";

function RecentlyAddedProducts() {
  const [products, setProducts] = useState();
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);

  useEffect(() => {
    // Handler to call on window resize
    function handleResize() {
      // Set window width to state
      setWindowWidth(window.innerWidth);
    }

    // Add event listener
    window.addEventListener("resize", handleResize);

    // Call handler right away so state gets updated with initial window size
    handleResize();

    // Remove event listener on cleanup
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    productService.getLatest().then((res) => setProducts(res));
  }, []);

  return (
    <div className={styles.recentProducts}>
      <h1 className={styles.title}>Recently added products:</h1>

      <div className={styles.containerProducts}>
        {windowWidth < 768 ? (
          <Carousel className={styles.carousel} data-bs-theme="dark" indicators={false} >
            {products === undefined ? (
              <p>Loading...</p>
            ) : (
              products.map((product, index) => (
                <Carousel.Item key={index}>
                  <RecentlyAddedProduct
                    key={product.id}
                    id={product.id}
                    name={product.name}
                    price={product.price}
                    image={product.image.imageData}
                  />
                </Carousel.Item>
              ))
            )}
          </Carousel>
        ) : products === undefined ? (
          <p>Loading...</p>
        ) : (
          products.map((product) => (
            <RecentlyAddedProduct
              key={product.id}
              id={product.id}
              name={product.name}
              price={product.price}
              image={product.image.imageData}
            />
          ))
        )}
      </div>
    </div>
  );
}

export default RecentlyAddedProducts;
