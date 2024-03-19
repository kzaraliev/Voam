import { useState } from "react";

import PrivacyPolicy from "../PrivacyPolicy/PrivacyPolicy"
import styles from "./Footer.module.css";

function Footer() {
    const [modalShow, setModalShow] = useState(false);

  return (
    <div className={styles.footer}>
        <PrivacyPolicy
        show={modalShow}
        onHide={() => setModalShow(false)}
      />
      <div className={styles.privacyPolicy} onClick={() => setModalShow(true)}>PRIVACY POLICY</div>
      <p className={styles.copyright}>
        Copyright © 2024 Voam Clothing. All Rights Reserved
      </p>
    </div>
  );
}

export default Footer;
