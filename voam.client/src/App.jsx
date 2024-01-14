import { Routes, Route } from "react-router-dom";

import Path from "./utils/paths.js";

import Navigation from './components/Navigation/Navigation';
import Home from './components/Home/Home';
import Footer from "./components/Footer/Footer.jsx";
import About from "./components/About/About.jsx";
import Contact from "./components/Contact/Contact.jsx";

function App() {

    return (
        <div>
            <Navigation />
            <Routes>
                <Route path={Path.Home} element={<Home />} />
                <Route path={Path.About} element={<About />} />
                <Route path={Path.Contacts} element={<Contact />} />
            </Routes>
            <Footer/>
        </div>
    );
}

export default App;