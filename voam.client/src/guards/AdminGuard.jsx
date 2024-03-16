import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";

import Path from "../utils/paths";
import AuthContext from "../context/authContext";

export default function AuthGuard() {
  const { isAdmin } = useContext(AuthContext);

  if (!isAdmin) {
    return <Navigate to={Path.Home} />;
  }

  return <Outlet />;
}
