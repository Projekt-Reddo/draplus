import * as React from "react";
import { Route, Routes } from "react-router-dom";

const BaseRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path="/" element={() => <></>} />
        </Routes>
    );
};

export default BaseRoutes;
