import * as React from "react";
import { RootStateOrAny, useSelector } from "react-redux";
import { Navigate, Outlet } from "react-router-dom";

interface AdminRouteProps {}

const AdminRoute: React.FC<AdminRouteProps> = () =>
{
    const user = useSelector((state: RootStateOrAny) => state.user);
    if (user.isAuthenticated && user.user.isAdmin) {
        return <Navigate to="/admin" />;
    }
    return <Outlet/>;
};

export default AdminRoute;

