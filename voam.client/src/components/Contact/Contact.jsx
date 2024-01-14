import { MdEmail } from "react-icons/md";
import { FaTiktok, FaInstagram } from "react-icons/fa";



import styles from "./Contact.module.css"

/*<a href="https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=voaminfo@gmail.com">voaminfo@gmail.com</a>*/

function Contact() {
    return (
        <div className={styles.contact}>
            <h1 className={styles.title}>Contact</h1>
            <p className={styles.subtitle}>Feel free to connect with us by sending a message or finding us on social media. We are eager to hear from you, and your thoughts and feedback are always welcome!</p>
            <ul className={styles.contacts}>
                <li className={styles.contactElement}>
                    <FaInstagram className={styles.icon} />
                    <h3 className={styles.contactTitle}>Instagram</h3>
                </li>
                <li className={styles.contactElement}>
                    <MdEmail className={styles.icon} />
                    <h3 className={styles.contactTitle}>E-mail</h3>
                </li>
                <li className={styles.contactElement}>
                    <FaTiktok className={styles.icon} />
                    <h3 className={styles.contactTitle}>Tiktok</h3>
                </li>
            </ul>
        </div>
    );
}

export default Contact;