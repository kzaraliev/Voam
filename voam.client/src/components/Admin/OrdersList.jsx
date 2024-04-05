import ListGroup from "react-bootstrap/ListGroup";
import Accordion from "react-bootstrap/Accordion";

import styles from "./Admin.module.css";

export default function OrdersList({ orders }) {
  return (
    <div className={styles.accordion}>
      <Accordion defaultActiveKey="0" bg="dark" data-bs-theme="dark">
        {orders.reverse().map((order, index) => (
          <Accordion.Item
            eventKey={index.toString()}
            key={index}
            className={styles.accordionItem}
          >
            <Accordion.Header>
              Order №{order.id} on {order.orderDate}
            </Accordion.Header>
            <Accordion.Body>
              <div>
                <strong>Full name:</strong> {order.fullName}
              </div>
              <div>
                <strong>Address:</strong> {order.econtAddress}
              </div>
              <div>
                <strong>City/Town:</strong> {order.city}
              </div>
              <div>
                <strong>Order date:</strong> {order.orderDate}
              </div>
              <div>
                <strong>Payment Method:</strong> {order.paymentMethod}
              </div>
              <ListGroup as="ol" numbered>
                {order.products.map((product, productIndex) => (
                  <ListGroup.Item
                    key={productIndex}
                    as="li"
                    action
                    variant="dark"
                    className="d-flex justify-content-between align-items-start"
                  >
                    <div className="ms-2 me-auto">
                      <div className="fw-bold">{product.name}</div>
                      Size: {product.sizeChar}, Quantity: {product.quantity}
                    </div>
                  </ListGroup.Item>
                ))}
              </ListGroup>
              <div className="mt-3">
                <strong>Total Price:</strong> ${order.totalPrice.toFixed(2)}
              </div>
            </Accordion.Body>
          </Accordion.Item>
        ))}
      </Accordion>
    </div>
  );
}
