import Modal from "react-bootstrap/Modal";

import styles from "./PrivacyPolicy.module.css"

export default function PrivacyPolicy(props) {
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
      
    >
      <Modal.Header closeButton className={styles.modal}>
        <Modal.Title id="contained-modal-title-vcenter">
          Privacy Policy
        </Modal.Title>
      </Modal.Header>
      <Modal.Body className={styles.modal}>
        <h5>Last Updated: 27.09.2023</h5>
        <p>
          Welcome to Voam Clothing! We value your trust and are committed to
          protecting your privacy. This Privacy Policy is designed to help you
          understand how we collect, use, disclose, and safeguard your personal
          information when you interact with our website, purchase our products,
          or engage with us in any other way. By using our services, you consent
          to the practices described in this Privacy Policy.
        </p>
        <h4>Information We Collect</h4>
        <p>We may collect various types of information, including:</p>
        <ul>
          <li>
            <b>Personal Information</b>: This includes information such as your
            name, email address, shipping address, billing information, and
            phone number, which we collect when you make a purchase.
          </li>
          <li>
            <b>Transaction Information</b>: When you make a purchase, we collect
            information related to your order, including product details,
            payment information, and shipping information.
          </li>
          <li>
            <b>Device Information</b>: We collect information about the device
            you use to access our website, including browser type and operating
            system.
          </li>
          <li>
            <b>Usage Information</b>: We may collect information about how you
            use our website, such as the pages you visit, your browsing history,
            and your interactions with our content and advertisements.
          </li>
          <li>
            <b>Communications</b>: We may collect information from your
            communications with us, including emails, chat messages, and
            customer support inquiries.
          </li>
          <li>
            <b>Cookies and Similar Technologies</b>: We use cookies and similar
            technologies to collect information about your browsing behavior and
            preferences.
          </li>
        </ul>
        <h4>How We Use Your Information</h4>
        <p>We use your information for various purposes, including:</p>
        <ul>
          <li>
            <b>Fulfillment of Orders</b>: To process and fulfill your orders,
            including shipping and handling.
          </li>
          <li>
            <b>Customer Support</b>: To provide customer support and respond to
            your inquiries.
          </li>
          <li>
            <b>Marketing and Promotions</b>: To send you promotional materials,
            newsletters, and updates about our products and services, if you
            have opted to receive them.
          </li>
          <li>
            <b>Analytics</b>: To analyze and improve our website, products, and
            services.
          </li>
          <li>
            <b>Legal Compliance</b>: To comply with applicable laws,
            regulations, and legal requests.
          </li>
          <li>
            <b>Protection</b>: To protect the security and integrity of our
            website and business.
          </li>
        </ul>
        <h4>Sharing Your Information</h4>
        <p>We may share your information with:</p>
        <ul>
          <li>
            <b>Service Providers</b>: We may share your information with
            third-party service providers who help us with various aspects of
            our business, such as payment processing, shipping, and marketing.
          </li>
          <li>
            <b>Legal Requirements</b>: We may disclose your information in
            response to legal requests or to comply with applicable laws and
            regulations.
          </li>
          <li>
            <b>Business Transfers</b>: In the event of a merger, acquisition, or
            sale of all or part of our business, your information may be
            transferred to the new owner.
          </li>
        </ul>
        <h4>Security</h4>
        <p>
          We take reasonable measures to protect your personal information, but
          no online data transmission is completely secure. You are responsible
          for maintaining the security of your account credentials.
        </p>
        <h4>Changes to this Privacy Policy</h4>
        <p>
          We may update this Privacy Policy from time to time. The latest
          version will always be available on our website, and the date of the
          last update will be noted at the top of the policy.
        </p>
        <h4>Contact Us</h4>
        <p>If you have any questions or concerns about this Privacy Policy or our data practices, please contact us at: <a href="https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=voaminfo@gmail.com" target="_blank" rel="noreferrer">voaminfo@gmail.com</a></p>
      </Modal.Body>
    </Modal>
  );
}
