import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import styles from "./ShoppingCart.module.css";
import { TiShoppingCart } from "react-icons/ti";
import CartItem from "./CartItem.jsx";
import * as shoppingCartService from "../../services/shoppingCartService.js";
import AuthContext from "../../context/authContext.jsx";
import Path from "../../utils/paths.js";

export default function ShoppingCart() {
  const [cartData, setCartData] = useState();
  const { userId } = useContext(AuthContext);
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
    shoppingCartService.get(userId).then((res) => setCartData(res)).catch(err => {
      console.log("401 Unauthorized")
      if (err === 401) {
        navigate(Path.Logout);
        alert("Something went wrong. Try login again")
      }
    });
  }, []);

  console.log(cartData);

  return (
    <div className={styles.shoppingCart}>
      <div className={styles.container}>
        <h1 className={styles.title}>Shopping Cart</h1>
        <ul className={styles.productsList}>
          {cartData === undefined ? (
            <p>Loading...</p>
          ) : cartData.cartItems.length == 0 ? (
            <>
              <TiShoppingCart className={styles.emptyCartIcon}/>
              <h2 className={styles.emptyCartTitle}>Your cart is empty</h2>
              <p className={styles.emptyCartText}>
                Looks like you haven't added anything to your cart.{" "}
                <Link to={Path.Items}> Go ahead & explore. </Link>
              </p>
            </>
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
        <button className={styles.submitButton} type="submit">
          Proceed to checkout (
          {cartData === undefined ? "" : cartData.totalPrice.toFixed(2)} lv.)
        </button>
      </div>
    </div>
  );
}
