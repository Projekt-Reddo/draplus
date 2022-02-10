import LoginWrapper from "components/LoginWrapper";
import * as React from "react";

interface LoginProps {}

const Login: React.FC<LoginProps> = () => {
    // return <div>Login</div>;
    return (
        <div
            className="h-screen w-screen flex items-center justify-center"
            style={{ backgroundColor: "var(--bg)" }}
        >
            <LoginWrapper />
        </div>
    );
};

export default Login;
