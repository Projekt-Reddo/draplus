// Libs
import * as React from "react";
import { Route, Routes } from "react-router-dom";

// Pages
import Login from "pages/Login";
import Board from "pages/Board";
import BoardList from "pages/BoardList";
import ErrorPage from "pages/ErrorPage";

import { useEffect } from "react";
import { init } from "utils/loginHandlers";
import { login } from "store/actions/index";
import { useDispatch } from "react-redux";
import { useNavigate, useLocation } from "react-router-dom";
import Loading from "components/Loading";

const BaseRoutes: React.FC = () => {
    const dispatch = useDispatch();
    // for navigate
    const [tempLocation, setTempLocation] = React.useState<string>("/");
    const location = useLocation();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = React.useState(true);
    useEffect(() => {
        var rs = init();
        if (!location.pathname.endsWith("/") || location.pathname === "") {
            setTempLocation(location.pathname);
            console.log(tempLocation);
        }
        if (rs) {
            const userStored = localStorage.getItem("user");
            const user = JSON.parse(userStored || "{}");
            const accessToken = localStorage.getItem("accessToken");
            dispatch(login({ ...user, accessToken: accessToken }));
            if (tempLocation !== "/") {
                navigate(tempLocation);
            }
        } else {
            navigate("/");
        }
        setIsLoading(false);
    }, []);

    if (isLoading) {
        return (
            <div className="h-screen w-screen bg-[color:var(--bg)] flex items-center justify-center">
                <Loading />
            </div>
        );
    }

    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/board/:boardId" element={<Board />} />
            <Route path="/board" element={<BoardList />} />
            <Route path="*" element={<ErrorPage />} />
        </Routes>
    );
};

export default BaseRoutes;
