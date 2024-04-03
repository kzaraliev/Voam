import { PiNewspaperClipping } from "react-icons/pi";
import { CiCalendarDate } from "react-icons/ci";
import { FaMoneyBill } from "react-icons/fa";
import { MdPayments } from "react-icons/md";
import { FaTreeCity,FaMapLocationDot  } from "react-icons/fa6";

export default function OrderDetails({id, date, city, address, payment, total, items}){
    return (
        <li>
            <p><PiNewspaperClipping/> Order number: {id}</p>
            <p><CiCalendarDate /> Date of placement: {date}</p>
            <p><FaTreeCity /> City/Town: {city}</p>
            <p><FaMapLocationDot /> Econt address: {address}</p>
            <p><MdPayments /> Payment method: {payment}</p>
            <p><FaMoneyBill /> Total amount {total.toFixed(2)} lv.</p>
            <ul>
                {items.map((item) => (<li key={item.id}>{item.name} - {item.sizeChar} x {item.quantity}</li>))}
            </ul>
        </li>
    )
}