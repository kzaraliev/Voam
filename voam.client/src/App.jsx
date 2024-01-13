import { Routes, Route } from "react-router-dom";

import Path from "./utils/paths.js";

import Navigation from './components/Navigation/Navigation';
import Home from './components/Home/Home';

function App() {

    return (
        <div>
            <Navigation />
            <Routes>
                <Route path={Path.Home} element={<Home />} />
            </Routes>
        </div>
    );
}

export default App;