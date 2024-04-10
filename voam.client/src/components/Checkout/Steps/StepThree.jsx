import { IoCheckmarkCircleSharp } from "react-icons/io5";
import styles from "./StepThree.module.css"

export default function StepThree() {
  return (
    <div className={styles.messageContainer}>
      <IoCheckmarkCircleSharp className={styles.icon}/>
      <h2 className={styles.title}>Order Successfully Placed</h2>
      <p className={styles.paragraph}>
        We're delighted to let you know that we've received your order. The team
        at Voam is now bustling to get everything ready just for you. We believe
        in making every part of your shopping experience memorable and
        enjoyable, and we can't wait for you to receive and love your purchase.
      </p>
      <p className={styles.paragraph}>
        You'll receive a confirmation email shortly with your order details.
        Should you have any questions or need further assistance, our customer
        service team is here to help at{" "}
        <a
          href="https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=voaminfo@gmail.com"
          target="_blank"
          rel="noreferrer"
        >
          voaminfo@gmail.com
        </a>
        .
      </p>
      <p className={styles.paragraph}>
        Thank you for choosing Voam. We're thrilled to have you as part of our
        community and look forward to serving you again
      </p>
    </div>
  );
}
