import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import LoginButton from "./components/LoginButton";

import { Provider } from "react-redux";
import store from "./store/store";
import Login from "pages/Login";

ReactDOM.render(
    <Provider store={store}>
        {/* <App /> */}
        <Login />
    </Provider>,
    document.getElementById("root")
);
