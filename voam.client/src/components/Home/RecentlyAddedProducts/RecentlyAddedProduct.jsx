import Card from "react-bootstrap/Card";
import Button from 'react-bootstrap/Button';

import styles from "./RecentlyAddedProducts.module.css";
import Path from "../../../utils/paths"

import { Link } from "react-router-dom";

export default function RecentlyAddedProduct({ id, name, price, image }) {

    const imgSrc = `data:image/jpeg;base64,${image}`;

    return (
        <Card className={styles.card} style={{ width: "24rem" }}>
            <Card.Img variant="top" src={imgSrc} className={styles.img} />
            <Card.Body className={styles.cardBody}>
                <Card.Title className={styles.cardTitle}>{name}</Card.Title>
                <Card.Subtitle className={["mb-2", styles.cardPrice]}>Price: {price} lv.</Card.Subtitle>
                <Button as={Link} to={`${Path.Items}/${id}`} variant="success">View Details</Button>
            </Card.Body>
        </Card>
    );
}