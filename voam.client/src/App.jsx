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

function App() {
  return (
    <AuthProvider>
      <div style={{ backgroundColor: "#3b444b" }}>
        <Navigation />
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
