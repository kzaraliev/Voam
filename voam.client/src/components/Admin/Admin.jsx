import { useEffect, useState } from "react";

import * as orderService from "../../services/orderService";
import Charts from "./Charts";

export default function Admin() {
  const [orders, setOrders] = useState([]);

  console.log(orders);

  useEffect(() => {
    orderService
      .getAll()
      .then(setOrders)
      .catch((err) => console.log(err));
  }, []);

  return (
    <div>
      <Charts orders={orders} />
    </div>
  );
}
