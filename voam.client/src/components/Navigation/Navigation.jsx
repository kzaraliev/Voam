import { useState, useContext } from "react";
import { Link } from "react-router-dom";

import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";

import Path from "../../utils/paths.js";
import AuthContext from "../../context/authContext.jsx";

function Navigation() {
  const [expanded, setExpanded] = useState(false);
  const { isAuthenticated, username, isAdmin } = useContext(AuthContext);

  const handleNavItemClick = () => {
    setExpanded(false);
  };

  return (
    <Navbar
      collapseOnSelect
      className="bg-body-tertiary"
      expand="lg"
      bg="dark"
      expanded={expanded}
      data-bs-theme="dark"
    >
      <Container>
        <Navbar.Brand as={Link} to={Path.Home}>
          Voam
        </Navbar.Brand>
        <Navbar.Toggle
          aria-controls="responsive-navbar-nav"
          onClick={() => setExpanded(!expanded)}
        />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to={Path.Home} onClick={handleNavItemClick}>
              Home
            </Nav.Link>
            <Nav.Link as={Link} to={Path.Items} onClick={handleNavItemClick}>
              Items
            </Nav.Link>
            <Nav.Link as={Link} to={Path.About} onClick={handleNavItemClick}>
              About
            </Nav.Link>
            <Nav.Link as={Link} to={Path.Contacts} onClick={handleNavItemClick}>
              Contact
            </Nav.Link>
            {isAdmin && (
              <>
                <Nav.Link
                  as={Link}
                  to={Path.CreateProduct}
                  onClick={handleNavItemClick}
                >
                  Create Product
                </Nav.Link>
              </>
            )}
          </Nav>
          <Nav>
            {isAdmin && (
              <>
                <Nav.Link
                  as={Link}
                  to={Path.Admin}
                  onClick={handleNavItemClick}
                >
                  Admin Panel
                </Nav.Link>
              </>
            )}
            {isAuthenticated && (
              <>
                {!isAdmin && (
                  <Nav.Link
                    as={Link}
                    to={Path.Profile}
                    onClick={handleNavItemClick}
                  >
                    Profile - {username}
                  </Nav.Link>
                )}
                <Nav.Link
                  as={Link}
                  to={Path.ShoppingCart}
                  onClick={handleNavItemClick}
                >
                  Shopping Cart
                </Nav.Link>
                <Nav.Link
                  as={Link}
                  to={Path.Logout}
                  onClick={handleNavItemClick}
                >
                  Logout
                </Nav.Link>
              </>
            )}
            {!isAuthenticated && (
              <>
                <Nav.Link
                  as={Link}
                  to={Path.Login}
                  onClick={handleNavItemClick}
                >
                  Login
                </Nav.Link>
                <Nav.Link
                  as={Link}
                  to={Path.Register}
                  onClick={handleNavItemClick}
                >
                  Register
                </Nav.Link>
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Navigation;
