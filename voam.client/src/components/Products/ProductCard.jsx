import Card from "react-bootstrap/Card";
import { Link } from "react-router-dom";
import { FaTshirt } from "react-icons/fa";

import styles from "./Products.module.css";
import Path from "../../utils/paths";

export default function ProductCard({ id, name, price, image }) {
  const imgSrc = `data:image/jpeg;base64,${image}`;

  return (
    <Link to={`${Path.Items}/${id}`} className={styles.cardLink}>
      <Card className={styles.cardBackground}>
        <Card.Img variant="top" src={imgSrc} className={styles.cardImg} alt="card image"/>
        <Card.Body className={styles.cardBody}>
          <Card.Title className={styles.cardTitle}>{name}</Card.Title>
          <Card.Text className={styles.price}>Price: {price} lv.</Card.Text>
        </Card.Body>
      </Card>
    </Link>
  );
}
