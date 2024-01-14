import Image from 'react-bootstrap/Image';

import styles from "./Home.module.css";
import banner from "../../assets/banner-f.png";
import RecentlyAddedProducts from './RecentlyAddedProducts/RecentlyAddedProducts';

function Home() {
    return (
        <div className={styles.home}>
            <h1 className={styles.title}>Voam</h1>
            <h2 className={styles.slogan}>Elevate Your Urban Vibe with Every Stride!</h2>
            <Image className={styles.banner} src={banner} fluid />
            <RecentlyAddedProducts/>
        </div>
    );
}

export default Home;