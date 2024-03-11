import { useState, useEffect, useContext } from "react";

import styles from "./ShoppingCart.module.css";
import CartItem from "./CartItem.jsx";
import * as shoppingCartService from "../../services/shoppingCartService.js";
import AuthContext from "../../context/authContext.jsx";

export default function ShoppingCart() {
  const [cartItems, setCartItems] = useState();
  const { userId } = useContext(AuthContext);

  console.log(cartItems);

  useEffect(() => {
    shoppingCartService.get(userId).then((res) => setCartItems(res));
  }, []);

  return (
    <div className={styles.shoppingCart}>
      <div className={styles.container}>
        <h1 className={styles.title}>Shopping Cart</h1>
        <ul className={styles.productsList}>
          {cartItems === undefined ? (
            <p>Loading...</p>
          ) : (
            cartItems.cartItems.map((cartItem) => (
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
        >
          Proceed to checkout ({cartItems === undefined ? "" : cartItems.totalAmount} lv.)
        </button>
      </div>
    </div>
  );
}
