import mouth from "../../assets/mouth-voam.png"
import logo from "../../assets/logo.png"
import styles from "./About.module.css";

import Figure from "react-bootstrap/Figure";

export default function About() {
    return (
        <div className={styles.aboutContainer}>
            <h1 className={styles.title}>About Book Worm</h1>
            <div className={styles.sections} style={{ marginBottom: "2px" }}>
                <div className={styles.text}>
                    <h2 className={styles.subtitle}>Our Story</h2>
                    <p>
                        At Book Worm, we believe in the transformative power of words. In a
                        world filled with constant noise, there's something magical about
                        the written word that has the ability to transport us, teach us, and
                        connect us. This belief is what inspired the creation of Book Worm -
                        a haven for book lovers, wordsmiths, and those who find solace in
                        the pages of a good book.
                    </p>
                </div>
                <Figure.Image width={450} src={mouth} />
            </div>
            <div className={styles.sections}>
                <Figure.Image width={371} src={logo} />
                <div className={styles.text}>
                    <h2 className={styles.subtitle}>Our Mission</h2>
                    <p>
                        Our mission is simple: to celebrate the beauty of literature and
                        foster a community of avid readers and writers. We strive to be more
                        than just a platform; we aspire to be a hub where words come to
                        life, ideas are shared, and stories unfold.
                    </p>
                </div>
            </div>
        </div>
    );
}
