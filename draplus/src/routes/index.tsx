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
import { login, logout } from "store/actions/index";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

const BaseRoutes: React.FC = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    useEffect(() => {
        var rs = init();
        if (rs) {
            const user = JSON.parse(localStorage.getItem("user") || "");
            const accessToken = localStorage.getItem("accessToken");
            dispatch(login({ ...user, accessToken: accessToken }));
            navigate(`/${user.boardId}`);
        }
    }, []);

    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/:boardId" element={<Board />} />
            <Route path="/list/board" element={<BoardList />} />
            <Route path="*" element={<ErrorPage />} />
        </Routes>
    );
};

export default BaseRoutes;
