import { useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";

import Path from "./utils/paths.js";
import { AuthProvider } from "./context/authContext.jsx";
import Navigation from "./components/Navigation/Navigation";
import Home from "./components/Home/Home";
import Footer from "./components/Footer/Footer.jsx";
import About from "./components/About/About.jsx";
import Contact from "./components/Contact/Contact.jsx";
import Login from "./components/Login/Login.jsx";
import Register from "./components/Register/Register.jsx";
import NotFound from "./components/NotFound/NotFound.jsx";
import ProductDetails from "./components/ProductDetails/ProductDetails.jsx";
import CreateProduct from "./components/CreateProduct/CreateProduct.jsx";
import Products from "./components/Products/Products.jsx";
import EditProduct from "./components/EditProduct/EditProduct.jsx";
import AuthGuard from "./guards/AuthGuard.jsx";
import AdminGuard from "./guards/AdminGuard.jsx";
import LoggedInGuard from "./guards/LoggedInGuard";
import Logout from "./components/Logout/Logout.jsx";
import ShoppingCart from "./components/ShoppingCart/ShoppingCart.jsx";
import Checkout from "./components/Checkout/Checkout.jsx";
import Profile from "./components/Profile/Profile.jsx";
import Admin from "./components/Admin/Admin.jsx";

// Define your SVG URL
import svgURL from "./assets/cross.svg";

// Function to generate random background styles
const generateSVGBackgrounds = (count) => {
  return Array.from({ length: count }).map((_, index) => ({
    backgroundImage: `url(${svgURL})`,
    backgroundRepeat: "no-repeat",
    backgroundSize: "100px 100px",
    position: "absolute",
    width: "100px",
    height: "100px",
    left: `${Math.random() * 90}%`,
    top: `${Math.random() * 90}%`,
    opacity: 0.3,
    transform: `rotate(${Math.random() * 360}deg)`,
    zIndex: -1,
  }));
};

function App() {
  const [backgroundCount, setBackgroundCount] = useState(20);

  useEffect(() => {
    // Function to handle resize events
    const handleResize = () => {
      if (window.innerWidth <= 768) {
        setBackgroundCount(8);
      } else if (window.innerWidth <= 1440) {
        setBackgroundCount(10);
      } else {
        setBackgroundCount(20);
      }
    };

    // Add event listener
    window.addEventListener("resize", handleResize);

    // Set initial count based on current window size
    handleResize();

    // Cleanup listener on component unmount
    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  const svgBackgrounds = generateSVGBackgrounds(backgroundCount);

  return (
    <AuthProvider>
      <div
        style={{
          position: "relative",
          backgroundColor: "#3b444b",
          zIndex: "1",
          overflow: "hidden",
        }}
      >
        <Navigation />
        {svgBackgrounds.map((style, index) => (
          <div key={index} style={style} />
        ))}
        <Routes>
          <Route path={Path.Home} element={<Home />} />
          <Route path={Path.Items} element={<Products />} />
          <Route path={`${Path.Items}/:id`} element={<ProductDetails />} />
          <Route path={Path.About} element={<About />} />
          <Route path={Path.Contacts} element={<Contact />} />
          <Route element={<LoggedInGuard />}>
            <Route path={Path.Register} element={<Register />} />
            <Route path={Path.Login} element={<Login />} />
          </Route>
          <Route element={<AdminGuard />}>
            <Route path={Path.CreateProduct} element={<CreateProduct />} />
            <Route path={Path.EditProduct} element={<EditProduct />} />
            <Route path={Path.Admin} element={<Admin />} />
          </Route>
          <Route element={<AuthGuard />}>
            <Route path={Path.ShoppingCart} element={<ShoppingCart />} />
            <Route path={Path.Checkout} element={<Checkout />} />
            <Route path={Path.Profile} element={<Profile />} />
            <Route path={Path.Logout} element={<Logout />}></Route>
          </Route>
          <Route path={Path.NotFound} element={<NotFound />} />
        </Routes>
        <Footer />
      </div>
    </AuthProvider>
  );
}

export default App;
