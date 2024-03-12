import { useEffect, useState } from "react";
import { HiMiniXMark } from "react-icons/hi2";

import * as productService from "../../services/productService";
import * as shoppingCartService from "../../services/shoppingCartService";
import defaultImg from "../../assets/hoodie_icon.png";
import styles from "./ShoppingCart.module.css";

export default function CartItem({ id, productId, quantity, sizeId, onDelete }) {
  const [product, setProduct] = useState();
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

  return (
    <>
      {product === undefined ? (
        <p>Loading...</p>
      ) : (
        <li className={styles.cartItem}>
          <HiMiniXMark className={styles.removeMark} onClick={handleDelete} />
          <img
            src={imgSrc}
            alt="Cart item image"
            className={styles.imgCartItem}
          />
          <p className={styles.productName}>{product.name}</p>
          <p>Size {size}</p>
          <p>Price: {product.price * quantity} lv.</p>
          <p>Quantity: {quantity}</p>
        </li>
      )}
    </>
  );
}
