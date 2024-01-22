import { Link } from "react-router-dom";

import Image from "react-bootstrap/Image";
import Path from "../../utils/paths";

import styles from "./Home.module.css";
import banner from "../../assets/banner-f.png";
import RecentlyAddedProducts from "./RecentlyAddedProducts/RecentlyAddedProducts";

function Home() {
  return (
    <div className={styles.home}>
      <h1 className={styles.title}>Voam</h1>
      <h2 className={styles.slogan}>
        Elevate Your Urban Vibe with Every Stride!
      </h2>
      <Image className={styles.banner} src={banner} fluid />
      <Link to={Path.Items} type="button" className={styles.shopNow}>
        Shop now
      </Link>
      <RecentlyAddedProducts />
    </div>
  );
}

export default Home;
