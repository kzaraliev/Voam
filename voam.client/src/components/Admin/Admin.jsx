import { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import * as orderService from "../../services/orderService";
import Charts from "./Charts";
import OrdersList from "./OrdersList";
import styles from "./Admin.module.css";
import AuthContext from "../../context/authContext.jsx";
import Path from "../../utils/paths.js";

export default function Admin() {
  const [orders, setOrders] = useState([]);
  const { logoutHandler } = useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    orderService
      .getAll()
      .then(setOrders)
      .catch((err) => {
        logoutHandler();
        navigate(Path.Login);
        alert("Ooops... Something went wrong. Try login again");
      });
  }, []);
  return (
    <div className={styles.adminPanelContainer}>
      <h1 className={styles.title}>Admin panel</h1>
      <p className={styles.subtitle}>*All information is from one year ago</p>
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
