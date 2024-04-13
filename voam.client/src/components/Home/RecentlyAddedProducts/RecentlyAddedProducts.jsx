import { useEffect, useState } from "react";
import styles from "./RecentlyAddedProducts.module.css";
import RecentlyAddedProduct from "./RecentlyAddedProduct";
import * as productService from "../../../services/productService";
import Carousel from "react-bootstrap/Carousel";
import Spinner from "react-bootstrap/Spinner";


function RecentlyAddedProducts() {
  const [products, setProducts] = useState();
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);

  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    function handleResize() {
      setWindowWidth(window.innerWidth);
    }
    window.addEventListener("resize", handleResize);
    handleResize();
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    productService.getLatest().then((res) => {
      setProducts(res);
      setLoading(false);
    });
  }, []);

  const renderCarousel = () => (
    <Carousel
      className={styles.carousel}
      data-bs-theme="dark"
      indicators={false}
    >
      {products.map((product, index) => (
        <Carousel.Item key={index}>
          <RecentlyAddedProduct
            key={product.id}
            id={product.id}
            name={product.name}
            price={product.price}
            image={product.image.imageData}
          />
        </Carousel.Item>
      ))}
    </Carousel>
  );

  const renderGrid = () =>
    products.map((product) => (
      <RecentlyAddedProduct
        key={product.id}
        id={product.id}
        name={product.name}
        price={product.price}
        image={product.image.imageData}
      />
    ));

  const renderLoadingIndicator = () => (
    <div className={styles.empty}>
      <Spinner animation="border" role="status" variant="light">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    </div>
  );

  return (
    <div className={styles.recentProducts}>
      <h1 className={styles.title}>Recently added products:</h1>

      <div className={styles.containerProducts}>
        {isLoading
          ? renderLoadingIndicator()
          : windowWidth < 768
          ? renderCarousel()
          : renderGrid()}
      </div>
    </div>
  );
}

export default RecentlyAddedProducts;
