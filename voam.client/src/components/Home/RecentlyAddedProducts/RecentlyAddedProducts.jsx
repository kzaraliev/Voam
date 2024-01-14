import styles from "./RecentlyAddedProducts.module.css";

function RecentlyAddedProducts() {
    /*Fetch recently added products*/
    return (
        <div className={styles.recentProducts}>
          <h1 className={styles.title}>Recently added products:</h1>
      </div>
  );
}

export default RecentlyAddedProducts;