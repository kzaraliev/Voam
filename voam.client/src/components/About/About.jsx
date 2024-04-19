import mouth from "../../assets/mouth-voam.png";
import logo from "../../assets/logo.png";
import styles from "./About.module.css";

export default function About() {
  return (
    <div className={styles.aboutContainer}>
      <h1 className={styles.title}>About Voam</h1>
      <div className={styles.sections} style={{ marginBottom: "2px" }}>
        <div className={styles.text}>
          <h2 className={styles.subtitle}>Our Story</h2>
          <p className={styles.paragraph}>
            Voam is a Clothing brand with roots located in Bulgaria. Within our
            first year we have managed to sell out multiple drops. We saw
            potential and decided to keep going. All the motivation comes from
            our clients and all the positive feedback we get. Our main goal is
            to deliver pleasurable experience to every single client along with
            a high-quality product representing the wave we are currently on.
          </p>
        </div>
        <img className={styles.image} src={mouth} alt="voam mouth"/>
      </div>
      <div className={styles.sections}>
        <img className={styles.image} src={logo} alt="voam logo"/>
        <div className={styles.text}>
          <h2 className={styles.subtitle}>Our Mission</h2>
          <p className={styles.paragraph}>
            Voam Clothing has established itself with its quality products and
            customer support. We want to show our customers that with each
            purchase they do not only receive a product, but they also
            contribute to the community that they are already part of- The Voam
            community. Every drop is symbolic and represents a new message we
            want to get through the people. We want everyone to find a piece
            he/she can see himself/herself in.
          </p>
        </div>
      </div>
    </div>
  );
}
