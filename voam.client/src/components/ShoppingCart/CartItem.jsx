import { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { HiMiniXMark } from "react-icons/hi2";
import {
  IoArrowForwardCircleOutline,
  IoArrowBackCircleOutline,
} from "react-icons/io5";

import * as productService from "../../services/productService";
import * as shoppingCartService from "../../services/shoppingCartService";
import defaultImg from "../../assets/hoodie_icon.png";
import styles from "./ShoppingCart.module.css";
import AuthContext from "../../context/authContext.jsx";
import Path from "../../utils/paths.js";

export default function CartItem({
  id,
  productId,
  quantity,
  sizeId,
  onDelete,
  hasBorder,
  onUpdate,
}) {
  const [product, setProduct] = useState();
  const { logoutHandler } = useContext(AuthContext);
  const navigate = useNavigate();

  const size = product
    ? product.sizes.find((size) => size.id === sizeId).sizeChar
    : null;
  const imgSrc = product?.images?.[0]
    ? `data:image/jpeg;base64,${product.images[0].imageData}`
    : defaultImg;

  useEffect(() => {
    productService.getOne(productId).then((res) => setProduct(res));
  }, []);

  function handleDelete() {
    const amountToDeduct = product.price * quantity;
    shoppingCartService.remove(id);
    onDelete(id, amountToDeduct);
  }

  function handleUpdateQuantity(change) {
    let newQuantity = quantity;
    if (change === "increase") {
      newQuantity = quantity + 1;
    } else if (change === "decrease" && quantity > 1) {
      // Prevents quantity from going below 1
      newQuantity = quantity - 1;
    }

    // Update the cart item with the new quantity if it has changed
    if (newQuantity !== quantity) {
      const data = {
        cartItemId: id,
        sizeId: sizeId,
        quantity: newQuantity,
      };

      shoppingCartService
        .updateQuantity(data)
        .then(() => {
          // Calculate the total price for this item based on the new quantity
          const updatedTotalPriceForItem = newQuantity * product.price;
          // Notify the ShoppingCart component of the change
          onUpdate(id, newQuantity, updatedTotalPriceForItem);
        })
        .catch((error) => {
          console.error("Failed to update quantity", error);

          if (error.Error === "Unauthorized") {
            logoutHandler();
            navigate(Path.Login);
            alert("Ooops... Something went wrong. Try login again");
          }
        });
    }
  }

  return (
    <>
      {product === undefined ? (
        <p>Loading...</p>
      ) : (
        <li
          className={`${styles.cartItem} ${
            hasBorder ? styles.borderItems : ""
          }`}
        >
          <div className={styles.productImgSection}>
            <HiMiniXMark className={styles.removeMark} onClick={handleDelete} />
            <img
              src={imgSrc}
              alt="Cart item image"
              className={styles.imgCartItem}
            />
          </div>
          <div className={styles.productData}>
            <p className={styles.productName}>{product.name}</p>
            <p className={styles.productSize}>Size {size}</p>
            <p className={styles.productPrice}>
              Price: {product.price * quantity} lv.
            </p>
            <div>
              <p className={styles.productQuantity}>
                Quantity:
                <span>
                  <IoArrowBackCircleOutline
                    onClick={() => handleUpdateQuantity("decrease")}
                  />
                </span>
                {quantity}
                <span>
                  <IoArrowForwardCircleOutline
                    onClick={() => handleUpdateQuantity("increase")}
                  />
                </span>
              </p>
            </div>
          </div>
        </li>
      )}
    </>
  );
}
