import Card from "react-bootstrap/Card";
import { Link } from "react-router-dom";
import { FaTshirt } from "react-icons/fa";

import styles from "./Products.module.css";
import Path from "../../utils/paths";

export default function ProductCard({ id, name, price, image }) {
    const imgSrc = `data:image/jpeg;base64,${image}`;

    return (
        <>
            <Card style={{ width: "22rem" }} className={styles.cardBackground}>
                <Card.Img
                    variant="top"
                    src={imgSrc}
                    className={styles.cardImg}
                />
                <Card.Body className={styles.cardBody}>
                    <Card.Title className={styles.cardTitle}>{name}</Card.Title>
                    <Card.Text className={styles.price}>Price: {price} lv.</Card.Text>
                    <Link to={`${Path.Items}/${id}`} className={styles.button}>
                        Details
                        <FaTshirt className={styles.icon} />
                    </Link>
                </Card.Body>
            </Card>
        </>
    );
}
