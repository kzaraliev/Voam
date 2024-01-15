import { Link } from "react-router-dom";

import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

import Path from "../../utils/paths.js"

function Navigation() {
    return (
        <Navbar expand="lg" className="bg-body-tertiary" bg="dark" data-bs-theme="dark">
            <Container>
                <Navbar.Brand as={Link} to={Path.Home}>Voam</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to={Path.Home}>Home</Nav.Link>
                        <Nav.Link as={Link} to={Path.Items}>Items</Nav.Link>
                        <Nav.Link as={Link} to={Path.About}>About</Nav.Link>
                        <Nav.Link as={Link} to={Path.Contacts}>Contact</Nav.Link>
                    </Nav>
                    <Nav>
                        <Nav.Link as={Link} to={Path.Profile}>Profile</Nav.Link>
                        <Nav.Link as={Link} to={Path.ShoppingCart}>Shopping Cart</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Navigation;