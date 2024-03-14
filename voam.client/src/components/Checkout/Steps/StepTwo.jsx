import { useState, useEffect, useContext } from "react";
import { useNavigate } from "react-router-dom";

import styles from "./StepTwo.module.css";
import CartItem from "./OrderItem.jsx";
import * as shoppingCartService from "../../../services/shoppingCartService.js";
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
        // Parse the string back into an object
        const checkoutData = JSON.parse(savedData);
        setBillingDetails(checkoutData);
      } catch (error) {
        // Handle errors if the stored string cannot be parsed into an object
        console.error("Failed to parse checkout data:", error);
      }
    }
  }, []);

  console.log(billingDetails);

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
              </ul>
            </div>
            <div className={styles.logoContainer}>
              <img className={styles.logo} src={logoImg} alt="Logo voam clothing" />
            </div>
          </div>
          <ul className={styles.productsList}>
            <h3 className={styles.productListTitle}>Your products:</h3>
            {cartData === undefined ? (
              <p>Loading...</p>
            ) : (
              cartData.cartItems.map((cartItem) => (
                <CartItem
                  key={cartItem.id}
                  id={cartItem.id}
                  productId={cartItem.productId}
                  quantity={cartItem.quantity}
                  sizeId={cartItem.sizeId}
                />
              ))
            )}
          </ul>
          <button
            className={styles.submitButton}
            type="submit"
            onClick={() => changeActiveStep(3)}
          >
            Place order (
            {cartData === undefined ? "" : cartData.totalPrice.toFixed(2)} lv.)
          </button>
        </div>
      </div>
    </div>
  );
}
