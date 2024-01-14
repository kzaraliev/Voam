import Image from 'react-bootstrap/Image';

import styles from "./Home.module.css";
import baner from "../../assets/baner-small.png";

function Home() {
    return (
        <div className={styles.home}>
            <h1 className={styles.title}>Voam</h1>
            <h2 className={styles.slogan}>Elevate Your Urban Vibe with Every Stride!</h2>
            <Image className={styles.baner} src={baner} thumbnail />

        </div>
    );
}

export default Home;