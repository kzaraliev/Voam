import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Carousel from "react-bootstrap/Carousel";

import styles from "./RecentlyAddedProducts.module.css";
import Path from "../../../utils/paths";

import { Link } from "react-router-dom";

export default function RecentlyAddedProduct({ id, name, price, image }) {
  const imgSrc = `data:image/jpeg;base64,${image}`;

  return (
    <Link to={`${Path.Items}/${id}`} className={styles.carouselItemContent}>
      <Card className={styles.card}>
        <Card.Img variant="top" src={imgSrc} className={styles.img} loading="lazy" alt="product image"/>
        <Card.Body className={styles.cardBody}>
          <Card.Title className={styles.cardTitle}>{name}</Card.Title>
          <Card.Subtitle className={["mb-2", styles.cardPrice]}>
            Price: {price} lv.
          </Card.Subtitle>
          <Button variant="success">View Details</Button>
        </Card.Body>
      </Card>
    </Link>
  );
}
