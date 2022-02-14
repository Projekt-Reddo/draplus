import "./App.css";
import { BrowserRouter as Router } from "react-router-dom";

import BaseRoutes from "routes";
import { useEffect } from "react";
import { init } from "utils/loginHandlers";
import { login, logout } from "store/actions/index";
import { useDispatch } from "react-redux";

function App() {
    const dispatch = useDispatch();
    useEffect(() => {
        var rs = init();
        if (rs) {
            // const user = JSON.parse(localStorage.getItem("user") || "");
            // dispatch(login(user));
        } else {
            dispatch(logout());
        }
    }, []);

    return (
        <Router>
            <BaseRoutes />
        </Router>
    );
}

export default App;
