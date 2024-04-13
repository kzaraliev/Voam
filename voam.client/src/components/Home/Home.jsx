import { Link } from "react-router-dom";

import Image from "react-bootstrap/Image";
import Path from "../../utils/paths";

import styles from "./Home.module.css";
import banner from "../../assets/banner.png";
import RecentlyAddedProducts from "./RecentlyAddedProducts/RecentlyAddedProducts";

function Home() {
  return (
    <div className={styles.home}>
      <h1 className={styles.title}>Voam</h1>
      <h2 className={styles.slogan}>
        Elevate Your Urban Vibe with Every Stride!
      </h2>
      <div className={styles.imageContainer}>
        <Image className={styles.banner} src={banner} fluid alt="banner voam" width={850} height={600}/>
      </div>
      <Link to={Path.Items} type="button" className={styles.shopNow}>
        Shop now
      </Link>
      <RecentlyAddedProducts />
    </div>
  );
}

export default Home;
