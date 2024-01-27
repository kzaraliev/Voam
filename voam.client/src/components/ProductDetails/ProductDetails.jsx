import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import * as productService from "../../services/productService";
import Path from "../../utils/paths";

export default function ProductDetails() {

    const { id } = useParams();
    const [product, setProduct] = useState({});
    const navigate = useNavigate();
    

useEffect(() => {
    productService.getOne(id).then(setProduct).catch((err) => {
        console.log(err)
        navigate(Path.Products);
    });
}, [id])


return (
    <div>
        <h1>{product.name}</h1>
    </div>
)
}