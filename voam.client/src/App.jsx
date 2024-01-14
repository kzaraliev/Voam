import { Routes, Route } from "react-router-dom";

import Path from "./utils/paths.js";

import Navigation from './components/Navigation/Navigation';
import Home from './components/Home/Home';
import Footer from "./components/Footer/Footer.jsx";

function App() {

    return (
        <div>
            <Navigation />
            <Routes>
                <Route path={Path.Home} element={<Home />} />
            </Routes>
            <Footer/>
        </div>
    );
}

export default App;