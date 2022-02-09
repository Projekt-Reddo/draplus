import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import LoginButton from "./Login_Component";

import { Provider } from "react-redux";
import store from "./store/store";

ReactDOM.render(
    <Provider store={store}>
        {/* <App /> */}
        <LoginButton></LoginButton>
    </Provider>,
    document.getElementById("root")
);
