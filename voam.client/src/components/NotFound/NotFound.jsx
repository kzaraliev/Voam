import { Link } from "react-router-dom";
import Nav from 'react-bootstrap/Nav';

import Path from '../../utils/paths';
import styles from "./NotFound.module.css";

export default function NotFound() {
  return (
    <div>
      <div id={styles.notfound}>
        <div className={styles.notfound}>
          <div className={styles.notfound_404}>
            <div></div>
            <h1>404</h1>
          </div>
          <h2>Page not found</h2>
          <p>
            The page you are looking for might have been removed had its name
            changed or is temporarily unavailable.
          </p>
          <Nav.Link as={Link} to={Path.Home}>Home page</Nav.Link>
        </div>
      </div>
    </div>
  );
}
