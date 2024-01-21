import { MdEmail } from "react-icons/md";
import { FaTiktok, FaInstagram } from "react-icons/fa";
import Accordion from 'react-bootstrap/Accordion';

import styles from "./Contact.module.css"

function Contact() {
    return (
        <div className={styles.contact}>
            <h1 className={styles.title}>Contact</h1>
            <p className={styles.subtitle}>Feel free to connect with us by sending a message or finding us on social media. We are eager to hear from you, and your thoughts and feedback are always welcome!</p>
            <ul className={styles.contacts}>
                <li className={styles.contactElement}>
                    <a href="https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=voaminfo@gmail.com" target="_blank" rel="noreferrer"><MdEmail className={styles.icon} /></a>
                    <h3 className={styles.contactTitle}>E-mail</h3>
                </li>
                <li className={styles.contactElement}>
                    <a href="https://www.instagram.com/voamclothing_/" target="_blank" rel="noreferrer"><FaInstagram className={styles.icon} /></a>
                    <h3 className={styles.contactTitle}>Instagram</h3>
                </li>
                <li className={styles.contactElement}>
                    <a href="https://www.tiktok.com/@voamclothing" target="_blank" rel="noreferrer"><FaTiktok className={styles.icon} /></a>
                    <h3 className={styles.contactTitle}>Tiktok</h3>
                </li>
            </ul>

            <Accordion bg="dark" data-bs-theme="dark" className={styles.accordion}>
                <h2 className={styles.accordionTitle}>Frequently Asked Questions</h2>
                <Accordion.Item eventKey="0">
                    <Accordion.Header>How long does delivery take?</Accordion.Header>
                    <Accordion.Body>
                        The delivery time depends on your location and the availability of the product. Once you place an order, you will receive a confirmation email with detailed information about your delivery estimate. Generally, our standard delivery time ranges from 3 to 5 business days.
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="1">
                    <Accordion.Header>Product quality?</Accordion.Header>
                    <Accordion.Body>
                        At Voam, we are committed to delivering products of the highest quality to our customers. Our team meticulously selects and sources materials from reputable suppliers, and our products undergo thorough testing and inspection throughout the manufacturing process. We adhere to stringent quality control standards to ensure that each item meets our quality benchmarks before it reaches your hands.
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>Can i return a product?</Accordion.Header>
                    <Accordion.Body>
                        Absolutely! We want you to love your purchase. If you are not completely satisfied with an item, you may return it within 30 days of the purchase date for a refund or exchange. Just send us a message via email - <a href="https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=voaminfo@gmail.com" target="_blank" rel="noreferrer">voaminfo@gmail.com</a>. The message must contain the order number!
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="3">
                    <Accordion.Header>What happens if a product is out of stock?</Accordion.Header>
                    <Accordion.Body>
                        If a product is out of stock, it is unlikely to be restocked. However, there is a small chance that we will do a new drop on an already sold out product. You can follow our instagram page - <a href="https://www.instagram.com/voamclothing_/" target="_blank" rel="noreferrer">@voamclothing_</a> to make sure you haven`t missed anything.
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </div>
    );
}

export default Contact;