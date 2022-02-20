import React from "react";
import LoginButton from "./LoginButton";

import Loading from "components/Loading";
import Notification from "components/Notification";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCoffee } from "@fortawesome/free-solid-svg-icons";

interface LoginWrapperProps {
    googleResponse: any;
    mutation: any;
    isShowing: boolean;
    toggle: () => void;
}
const LoginWrapper: React.FC<LoginWrapperProps> = ({
    googleResponse,
    mutation,
    isShowing,
    toggle,
}) => {
<<<<<<< HEAD
    // if (isShowing) {
    //     // return <Modal toggle={toggle}></Modal>;
    //     return (
    //         <Notification
    //             icon="circle-check"
    //             title="Login failed"
    //             // message="Now you can share this link"
    //             toggle={isShowing}
    //             setToggle={toggle}
    //         />
    //     );
    // }
=======
    if (isShowing) {
        return <div>Error</div>;
        // <Modal toggle={toggle}></Modal>;
    }
>>>>>>> b9f0d7343a6849ebf071e6eda26fac44c96920d2
    return (
        <div
            className="h-1/2 w-1/4 flex flex-col items-center justify-between border-rounded app-shadow"
            style={{
                backgroundColor: "var(--element-bg)",
                minWidth: "350px",
                maxWidth: "500px",
            }}
        >
            <div className="h-auto mx-2 content-center text-center">
                <FontAwesomeIcon
                    className="mx-2 mt-6 mb-3 text-white"
                    icon={faCoffee}
                    size="2x"
                />
                <p className="text-2xl text-white font-bold">
                    Login To Draplus
                </p>
            </div>
            {mutation.isLoading ? (
                <Loading></Loading>
            ) : isShowing ? (
                <Notification
                    icon="circle-exclamation"
                    iconColor="text-red-400"
                    title="Login failed"
                    // message="Now you can share this link"
                    toggle={isShowing}
                    setToggle={toggle}
                />
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
