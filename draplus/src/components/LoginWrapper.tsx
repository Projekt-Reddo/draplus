import React from "react";
import LoginButton from "./LoginButton";

import Loading from "components/Loading";
import Modal from "components/Modal";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCoffee } from "@fortawesome/free-solid-svg-icons";

import {
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";

interface LoginWrapperProps {
    googleResponse: GoogleLoginResponse | GoogleLoginResponseOffline;
    mutation: any;
    isShowing: boolean;
    toggle: () => void;
}
const LoginWrapper: React.FC<any> = ({
    googleResponse,
    mutation,
    isShowing,
    toggle,
}) => {
    if (isShowing) {
        return <Modal toggle={toggle}></Modal>;
    }
    return (
        <div
            className="h-2/4 w-1/4 flex flex-col items-center justify-between border-rounded app-shadow"
            style={{ backgroundColor: "var(--element-bg)" }}
        >
            <FontAwesomeIcon className="mx-2 my-2" icon={faCoffee} size="6x" />
            <div className="mx-2 my-1 content-center text-center text-3xl text-white font-bold">
                <p>Login To Draplus</p>
            </div>
            {mutation.isLoading ? (
                <Loading></Loading>
            ) : (
                <LoginButton googleResponse={googleResponse}></LoginButton>
            )}
            <div
                className="mx-2 mb-9 content-center text-center"
                style={{ color: "var(--text-small)", fontSize: "60%" }}
            >
                <p>
                    By using Draplus you are agreeing to our <br /> terms of
                    services and privacy policy
                </p>
            </div>
        </div>
    );
};

export default LoginWrapper;
