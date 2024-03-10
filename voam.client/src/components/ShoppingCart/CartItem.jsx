import { useEffect, useState } from "react";

import * as productService from "../../services/productService"
import defaultImg from "../../assets/hoodie_icon.png" 


export default function CartItem({id, productId, quantity, sizeId}) {
    const [product, setProduct] = useState();
    const size = product ? product.sizes.find(size => size.id === sizeId).sizeChar : null;
    const imgSrc = product?.images?.[0] ? `data:image/jpeg;base64,${product.images[0].imageData}` : defaultImg; 

    console.log(product)

    useEffect(() => {
        productService.getOne(productId).then(res => setProduct(res))
    }, []);

    return(
        <>
            {product === undefined ? <p>Loading...</p> :
            <li>
                <img src={imgSrc} alt="Cart item image" />
                <p>{product.name}</p>
                <p>Size {size}</p>
                <p>Price: {product.price * quantity} lv.</p>
                <p>Quantity: {quantity}</p>
            </li>
            } 
        </>
    )
}