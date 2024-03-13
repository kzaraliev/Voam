import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import styles from "./StepTwo.module.css";
import { TiShoppingCart } from "react-icons/ti";
import CartItem from "./OrderItem.jsx";
import * as shoppingCartService from "../../../services/shoppingCartService.js";
import AuthContext from "../../../context/authContext.jsx";
import Path from "../../../utils/paths.js";

export default function StepTwo({ changeActiveStep }) {
  const [cartData, setCartData] = useState();
  const { userId, logoutHandler } = useContext(AuthContext);
  const navigate = useNavigate();

  function handleItemDelete(itemId, amountToDeduct) {
    setCartData((currentItems) => {
      const updatedTotalAmount = currentItems.totalPrice - amountToDeduct;

      return {
        ...currentItems,
        cartItems: currentItems.cartItems.filter((item) => item.id !== itemId),
        totalPrice: updatedTotalAmount,
      };
    });
  }

  useEffect(() => {
    shoppingCartService
      .get(userId)
      .then((res) => setCartData(res))
      .catch((err) => {
        console.log("401 Unauthorized");
        if (err === 401) {
          logoutHandler();
          navigate(Path.Login);
          alert("Something went wrong. Try login again");
        }
      });
  }, []);

  return (
    <>
      <div className={styles.shoppingCart}>
        <div className={styles.container}>
          <h1 className={styles.title}>Order preview</h1>
          <ul className={styles.productsList}>
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
                  onDelete={handleItemDelete}
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
    </>
  );
}
