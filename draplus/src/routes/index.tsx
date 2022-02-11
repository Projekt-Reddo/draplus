// Libs
import * as React from "react";
import { Route, Routes } from "react-router-dom";

// Pages
import Login from "pages/Login";
import Board from "pages/Board";
import BoardList from "pages/BoardList";

const BaseRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/board" element={<Board />} />
            <Route path="/list/board" element={<BoardList />} />
        </Routes>
    );
};

export default BaseRoutes;
