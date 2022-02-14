import React from "react";
import LoginButton from "./LoginButton";

const LoginWrapper: React.FC<any> = () => {
    return (
        <div
            className="h-2/4 w-1/4 flex flex-col items-center justify-between border-rounded app-shadow"
            style={{ backgroundColor: "var(--element-bg)" }}
        >
            <div></div>
            <div className="mx-2 my-2 content-center text-center text-3xl text-white font-bold">
                <p>Login To Draplus</p>
            </div>
            <LoginButton></LoginButton>
            <div
                className="mx-2 mb-9 content-center text-center"
                style={{ color: "var(--text-small)", fontSize: "60%" }}
            >
                <p className="">
                    By using Draplus you are agreeing to our <br /> terms of
                    services and privacy policy
                </p>
            </div>
        </div>
    );
};

export default LoginWrapper;
