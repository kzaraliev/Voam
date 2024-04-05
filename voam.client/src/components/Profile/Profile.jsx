import { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FaUser, FaPhoneAlt } from "react-icons/fa";
import { MdEmail } from "react-icons/md";
import { FaClipboardUser } from "react-icons/fa6";

import {
  FaUserSecret,
  FaUserNinja,
  FaUserAstronaut,
  FaUserTie,
} from "react-icons/fa";

import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";

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

  // List of icons
  const icons = [
    <FaUserSecret />,
    <FaUserNinja />,
    <FaUserAstronaut />,
    <FaUserTie />,
  ];

  // State to hold the current icon
  const [currentIcon, setCurrentIcon] = useState();

  // Function to get a random icon
  const getRandomIcon = () => {
    const randomIndex = Math.floor(Math.random() * icons.length);
    return icons[randomIndex];
  };

  useEffect(() => {
    authService
      .getUserPhoneNumber(userId)
      .then((res) => {
        setUser({
          email: email,
          fullName: username,
          phoneNumber: res,
        });
        setCurrentIcon(getRandomIcon());
      })
      .catch((err) => console.log(err));

    orderService
      .get(userId)
      .then(setOrders)
      .catch((err) => console.log(err));
  }, [userId]);

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Profile page</h1>
      <section>
        <h2 className={styles.sectionTitle}>Personal data:</h2>
        <Card bg="dark" text="white" className={styles.personalDataCard}>
          <div className={styles.personalDataCardIcon}>{currentIcon}</div>
          <Card.Body>
            <ListGroup
              className="list-group-flush"
              bg="dark"
              data-bs-theme="dark"
            >
              <ListGroup.Item>
                <FaClipboardUser className={styles.icon} /> Full name:{" "}
                {user.fullName}
              </ListGroup.Item>
              <ListGroup.Item>
                <MdEmail className={styles.icon} /> Email: {user.email}
              </ListGroup.Item>
              <ListGroup.Item>
                <FaPhoneAlt className={styles.icon} /> Phone number:{" "}
                {user.phoneNumber}
              </ListGroup.Item>
            </ListGroup>
          </Card.Body>
        </Card>
      </section>
      <section>
        <h2 className={styles.sectionTitle}>Order history:</h2>
        <ul className={styles.ordersList}>
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
    </div>
  );
}
