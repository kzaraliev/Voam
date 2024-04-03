import { useState, useEffect, useContext } from "react";
import { useNavigate } from "react-router-dom";

import styles from "./StepTwo.module.css";
import OrderItem from "./OrderItem.jsx";
import * as shoppingCartService from "../../../services/shoppingCartService.js";
import * as orderService from "../../../services/orderService.js";
import AuthContext from "../../../context/authContext.jsx";
import Path from "../../../utils/paths.js";
import logoImg from "../../../assets/logo.png";

export default function StepTwo({ changeActiveStep }) {
  const [cartData, setCartData] = useState();
  const [billingDetails, setBillingDetails] = useState({});
  const { userId, logoutHandler } = useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    shoppingCartService
      .get(userId)
      .then((res) => setCartData(res))
      .catch((err) => {
        logoutHandler();
        navigate(Path.Login);
        alert("Ooops... Something went wrong. Try login again");
      });
    const savedData = localStorage.getItem("checkout-data");
    if (savedData) {
      try {
        const checkoutData = JSON.parse(savedData);
        setBillingDetails(checkoutData);
      } catch (error) {
        console.error("Failed to parse checkout data:", error);
      }
    }
  }, []);

  function submitOrder() {
    const data = {
      fullName: billingDetails.fullName,
      email: billingDetails.email,
      phoneNumber: billingDetails.phone,
      EcontOffice: billingDetails.econt,
      city: billingDetails.city,
      paymentMethod: billingDetails.payment,
      products: cartData.cartItems,
      totalPrice: cartData.totalPrice,
    };

    orderService
      .add(userId, data)
      .then(localStorage.removeItem("checkout-data"));
  }

  return (
    <div className={styles.orderPreviewContainer}>
      <div className={styles.shoppingCart}>
        <div className={styles.container}>
          <h1 className={styles.title}>Order details</h1>
          <div className={styles.shippingInfoContainer}>
            <div>
              <h3>Billing info:</h3>
              <ul className={styles.shippingInfo}>
                <li>
                  <span className={styles.shippingLabel}>Name</span>:{" "}
                  {billingDetails.fullName}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Email</span>:{" "}
                  {billingDetails.email}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Phone</span>:{" "}
                  {billingDetails.phone}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Econt Office</span>:{" "}
                  {billingDetails.econt}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Town/City</span>:{" "}
                  {billingDetails.city}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Payment Method</span>:{" "}
                  {billingDetails.payment}
                </li>
              </ul>
            </div>
            <div className={styles.logoContainer}>
              <img
                className={styles.logo}
                src={logoImg}
                alt="Logo voam clothing"
              />
            </div>
          </div>
          <ul className={styles.productsList}>
            <h3 className={styles.productListTitle}>Your products:</h3>
            {cartData === undefined ? (
              <p>Loading...</p>
            ) : (
              cartData.cartItems.map((cartItem, index) => (
                <OrderItem
                  key={cartItem.id}
                  id={cartItem.id}
                  productId={cartItem.productId}
                  quantity={cartItem.quantity}
                  sizeId={cartItem.sizeId}
                  hasBorder={
                    index !== cartData.cartItems.length - 1 &&
                    cartData.cartItems.length > 1
                  }
                />
              ))
            )}
          </ul>
          <button
            className={styles.submitButton}
            type="submit"
            onClick={() => {
              changeActiveStep(3);
              submitOrder();
            }}
          >
            Place order (
            {cartData === undefined ? "" : cartData.totalPrice.toFixed(2)} lv.)
          </button>
        </div>
      </div>
    </div>
  );
}
