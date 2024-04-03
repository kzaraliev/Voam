import { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FaUser, FaPhoneAlt } from "react-icons/fa";
import { MdEmail } from "react-icons/md";

import OrderDetails from "./OrderDetails";
import Path from "../../utils/paths";
import * as authService from "../../services/authService";
import * as orderService from "../../services/orderService";
import AuthContext from "../../context/authContext";
import styles from "./Profile.module.css";

export default function Profile() {
  const { email, username, userId } = useContext(AuthContext);
  const [user, setUser] = useState({});
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    authService
      .getUserPhoneNumber(userId)
      .then((res) =>
        setUser({
          email: email,
          fullName: username,
          phoneNumber: res,
        })
      )
      .catch((err) => console.log(err));

    orderService
      .get(userId)
      .then(setOrders)
      .catch((err) => console.log(err));
  }, [userId]);

  return (
    <div className={styles.container}>
      <section>
        <h2>Personal data</h2>
        <ul>
          <li>
            <FaUser /> Full name: {user.fullName}
          </li>
          <li>
            <MdEmail /> Email: {user.email}
          </li>
          <li>
            <FaPhoneAlt /> Phone number: {user.phoneNumber}
          </li>
        </ul>
      </section>
      <section>
        <h2>Order history</h2>
        <ul>
          {orders.map((order) => (
            <OrderDetails
              key={order.id}
              id={order.id}
              date={order.orderDate}
              city={order.city}
              address={order.econtAddress}
              payment={order.paymentMethod}
              total={order.totalPrice}
              items={order.products}
            />
          ))}
        </ul>
      </section>
      <Link as={Link} to={Path.Logout}>
        Logout
      </Link>
    </div>
  );
}
