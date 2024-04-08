import { useContext, useEffect, useState } from "react";
import { GiDesert } from "react-icons/gi";
import { FaPhoneAlt } from "react-icons/fa";
import { MdEmail } from "react-icons/md";
import { FaClipboardUser } from "react-icons/fa6";

import {
  FaUserSecret,
  FaUserNinja,
  FaUserAstronaut,
  FaUserTie,
} from "react-icons/fa";

import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import Pagination from "react-bootstrap/Pagination";
import Dropdown from "react-bootstrap/Dropdown";

import OrderDetails from "./OrderDetails";
import Path from "../../utils/paths";
import * as authService from "../../services/authService";
import * as orderService from "../../services/orderService";
import AuthContext from "../../context/authContext";
import styles from "./Profile.module.css";
import { Link } from "react-router-dom";

export default function Profile() {
  const { email, username, userId } = useContext(AuthContext);
  const [sortText, setSortText] = useState("");
  const [user, setUser] = useState({});
  const [orders, setOrders] = useState([]);
  const [pageSize, setPageSize] = useState(3);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPageNumber, setTotalPageNumber] = useState(1);

  // List of icons
  const icons = [
    <FaUserSecret />,
    <FaUserNinja />,
    <FaUserAstronaut />,
    <FaUserTie />,
  ];

  // State to hold the current icon
  const [currentIcon, setCurrentIcon] = useState();

  // Function to get a random icon
  const getRandomIcon = () => {
    const randomIndex = Math.floor(Math.random() * icons.length);
    return icons[randomIndex];
  };

  useEffect(() => {
    authService
      .getUserPhoneNumber(userId)
      .then((res) => {
        setUser({
          email: email,
          fullName: username,
          phoneNumber: res,
        });
        setCurrentIcon(getRandomIcon());
      })
      .catch((err) => console.log(err));

    orderService
      .get(userId, pageSize, pageNumber)
      .then((res) => {
        const { items, currentPage, totalPages, pageSize } = res;
        setTotalPageNumber(totalPages);
        setPageNumber(currentPage);
        setPageSize(pageSize)

        console.log(totalPageNumber);
        console.log(pageNumber);
        setOrders(items); // Set items separately
      })
      .catch((err) => console.log(err));
  }, [userId, pageNumber, pageSize]);

  const handlePageSizeChange = (newSize) => {
    if (newSize !== pageSize) {
      setPageSize(newSize);
      setPageNumber(1); // Reset to page 1 if page size changes
      setSortText(newSize)
    }
  };

  const handlePageNumberChange = (newPageNumber) => {
    setPageNumber(newPageNumber);
  };

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Profile page</h1>
      <section>
        <h2 className={styles.sectionTitle}>Personal data:</h2>
        <Card bg="dark" text="white" className={styles.personalDataCard}>
          <div className={styles.personalDataCardIcon}>{currentIcon}</div>
          <Card.Body>
            <ListGroup
              className="list-group-flush"
              bg="dark"
              data-bs-theme="dark"
            >
              <ListGroup.Item>
                <FaClipboardUser className={styles.icon} /> Full name:{" "}
                {user.fullName}
              </ListGroup.Item>
              <ListGroup.Item>
                <MdEmail className={styles.icon} /> Email: {user.email}
              </ListGroup.Item>
              <ListGroup.Item>
                <FaPhoneAlt className={styles.icon} /> Phone number:{" "}
                {user.phoneNumber}
              </ListGroup.Item>
            </ListGroup>
          </Card.Body>
        </Card>
      </section>
      <section>
        <h2 className={styles.sectionTitle}>Order history:</h2>
        {orders.length > 0 ? (
          <div className={styles.availableOrders}>
            <Dropdown
              className={styles.dropdown}
              onSelect={handlePageSizeChange}
              bg="dark"
              data-bs-theme="dark"
            >
              <Dropdown.Toggle className={styles.dropdownButton}>
                {sortText !== "" ? `Show by: ${sortText}` : "Show by"}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                <Dropdown.Item eventKey="3">3</Dropdown.Item>
                <Dropdown.Item eventKey="5">5</Dropdown.Item>
                <Dropdown.Item eventKey="8">8</Dropdown.Item>
                <Dropdown.Item eventKey="10">10</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
            <ul className={styles.ordersList}>
              {orders.map((order) => (
                <OrderDetails
                  key={order.id}
                  id={order.id}
                  date={order.orderDate}
                  city={order.city}
                  address={order.econtAddress}
                  payment={order.paymentMethod}
                  total={order.totalPrice}
                  items={order.products}
                />
              ))}
            </ul>
            <Pagination bg="dark" data-bs-theme="dark">
              <Pagination.First
                onClick={() => handlePageNumberChange(1)}
                disabled={pageNumber === 1}
              />
              <Pagination.Prev
                onClick={() => handlePageNumberChange(pageNumber - 1)}
                disabled={pageNumber === 1}
              />
              {pageNumber !== 1 && (
                <Pagination.Item
                  onClick={() => handlePageNumberChange(pageNumber - 1)}
                >
                  {pageNumber - 1}
                </Pagination.Item>
              )}
              <Pagination.Item active>{pageNumber}</Pagination.Item>
              {pageNumber !== totalPageNumber && (
                <Pagination.Item
                  onClick={() => handlePageNumberChange(pageNumber + 1)}
                >
                  {pageNumber + 1}
                </Pagination.Item>
              )}
              <Pagination.Next
                onClick={() => handlePageNumberChange(pageNumber + 1)}
                disabled={pageNumber === totalPageNumber}
              />
              <Pagination.Last
                onClick={() => handlePageNumberChange(totalPageNumber)}
                disabled={pageNumber === totalPageNumber}
              />
            </Pagination>
          </div>
        ) : (
          <div className={styles.noOrders}>
            <GiDesert className={styles.desertIcon} />
            <p>
              It looks like your order history is as untouched as a desert
              landscape. But don't worry, this just means you're moments away
              from discovering something wonderful. Our shop is brimming with
              unique finds and must-have items waiting to be explored. Why not
              take this opportunity to treat yourself or find the perfect gift
              for a friend?
            </p>
            <p>
              Dive into our collection and find your oasis today. With special
              offers just a few clicks away, you're bound to find something that
              quenches your thirst for the extraordinary. Let's turn this barren
              desert into a fertile ground of delightful discoveries!
            </p>
            <Link to={Path.Items}>Go ahead & explore.</Link>
          </div>
        )}
      </section>
    </div>
  );
}
