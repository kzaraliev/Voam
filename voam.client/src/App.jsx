import { Routes, Route } from "react-router-dom";

import Path from "./utils/paths.js";

import Navigation from './components/Navigation/Navigation';
import Home from './components/Home/Home';
import Footer from "./components/Footer/Footer.jsx";
import About from "./components/About/About.jsx";
import Contact from "./components/Contact/Contact.jsx";
import Login from "./components/Login/Login.jsx";

function App() {

    return (
        <div style={{ backgroundColor: "#3b444b" }}>
            <Navigation />
            <Routes>
                <Route path={Path.Home} element={<Home />} />
                <Route path={Path.About} element={<About />} />
                <Route path={Path.Contacts} element={<Contact />} />
                <Route path={Path.Login} element={<Login />} />
            </Routes>
            <Footer />
        </div>
    );
}

export default App;