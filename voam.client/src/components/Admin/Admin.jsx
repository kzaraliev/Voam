import { useEffect, useState } from "react";

import * as orderService from "../../services/orderService";
import Charts from "./Charts";
import OrdersList from "./OrdersList";
import styles from "./Admin.module.css";

export default function Admin() {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    orderService
      .getAll()
      .then(setOrders)
      .catch((err) => console.log(err));
  }, []);

  console.log(orders)

  return (
    <div className={styles.adminPanelContainer}>
      <h1 className={styles.title}>Admin panel</h1>
      <section>
        <h2 className={styles.sectionTitle}>Orders:</h2>
        <OrdersList orders={orders} />
      </section>
      <section>
        <h2 className={styles.sectionTitle}>Statistics for Voam:</h2>
        <Charts orders={orders} />
      </section>
    </div>
  );
}
