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

  function handleQuantityUpdate(itemId, newQuantity, updatedTotalPriceForItem) {
    setCartData((currentData) => {
      const updatedCartItems = currentData.cartItems.map((item) => {
        if (item.id === itemId) {
          // Update the quantity and price for this specific item
          return {
            ...item,
            quantity: newQuantity,
            totalPrice: updatedTotalPriceForItem,
          };
        }
        return item;
      });

      // Sum the total prices for all items to get the updated cart total
      const updatedTotalPrice = updatedCartItems.reduce(
        (total, item) => total + item.totalPrice,
        0
      );

      return {
        ...currentData,
        cartItems: updatedCartItems,
        totalPrice: updatedTotalPrice,
      };
    });
  }

  useEffect(() => {
    shoppingCartService
      .get(userId)
      .then((res) => setCartData(res))
      .catch((err) => {
        logoutHandler();
        navigate(Path.Login);
        alert("Ooops... Something went wrong. Try login again");
      });
  }, []);

  return (
    <div className={styles.shoppingCart}>
      <div className={styles.container}>
        <h1 className={styles.title}>Shopping Cart</h1>
        <ul className={styles.productsList}>
          {cartData === undefined ? (
            <p>Loading...</p>
          ) : cartData.cartItems.length == 0 ? (
            <>
              <TiShoppingCart className={styles.emptyCartIcon} />
              <h2 className={styles.emptyCartTitle}>Your cart is empty</h2>
              <p className={styles.emptyCartText}>
                Looks like you haven't added anything to your cart.{" "}
                <Link to={Path.Items}> Go ahead & explore. </Link>
              </p>
            </>
          ) : (
            cartData.cartItems.map((cartItem, index) => (
              <CartItem
                key={cartItem.id}
                id={cartItem.id}
                productId={cartItem.productId}
                quantity={cartItem.quantity}
                sizeId={cartItem.sizeId}
                onDelete={handleItemDelete}
                pricePerItem={cartItem.price}
                hasBorder={
                  index !== cartData.cartItems.length - 1 &&
                  cartData.cartItems.length > 1
                }
                onUpdate={handleQuantityUpdate}
              />
            ))
          )}
        </ul>
        {cartData === undefined ? (
          <p>Loading</p>
        ) : (
          cartData.cartItems.length != 0 && (
            <Link to={Path.Checkout}>
              <button className={styles.submitButton} type="submit">
                Proceed to checkout (
                {cartData === undefined ? "" : cartData.totalPrice.toFixed(2)}{" "}
                lv.)
              </button>
            </Link>
          )
        )}
      </div>
    </div>
  );
}
