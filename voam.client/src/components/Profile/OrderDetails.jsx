import { GiClothes } from "react-icons/gi";
import { CiCalendarDate } from "react-icons/ci";
import { FaMoneyBill } from "react-icons/fa";
import { MdPayments } from "react-icons/md";
import { FaTreeCity, FaMapLocationDot } from "react-icons/fa6";
import Card from "react-bootstrap/Card";

import styles from "./Profile.module.css";

export default function OrderDetails({
  id,
  date,
  city,
  address,
  payment,
  total,
  items,
}) {
  return (
    <li className={styles.orderItem}>
      <Card bg="dark" text="white">
        <Card.Header>Order №{id}</Card.Header>
        <Card.Body>
          <p>
            <CiCalendarDate /> Date of placement: {date}
          </p>
          <p>
            <FaTreeCity /> City/Town: {city}
          </p>
          <p>
            <FaMapLocationDot /> Econt address: {address}
          </p>
          <p>
            <MdPayments /> Payment method: {payment}
          </p>
          <p>
            <FaMoneyBill /> Total amount {total.toFixed(2)} lv.
          </p>
          <div>
            <GiClothes />
            Products:
            <ul>
              {items.map((item) => (
                <li key={item.id}>
                  {item.name} - {item.sizeChar} x {item.quantity}
                </li>
              ))}
            </ul>
          </div>
        </Card.Body>
      </Card>
    </li>
  );
}
