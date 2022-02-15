import * as React from "react";
import { default as data_env } from "../config/config.json";
import {
    GoogleLogin,
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";

import "styles/GoogleLoginButton.css";

interface LoginButtonProps {
    googleResponse: () => void;
}
interface IAuthResponse {
    token: string;
}
type ResponseUser = {
    user: JSON;
};
type queryType = {
    manual: boolean;
};
const LoginButton: React.FC<LoginButtonProps> = ({ googleResponse }) => {
    // function fetchData(options: Object) {
    //     console.log(options);
    //     fetch(data_env.GOOGLE_AUTH_CALLBACK_URL, options)
    //         .then((r) => {
    //             r.json().then((user) => {
    //                 dispatch(login(user));
    //                 navigate(`/${user.boardId}`);
    //             });
    //         })
    //         .catch((e) => {
    //             console.error(e);
    //         });
    // }

    const styles = { borderRadius: 50, width: "100%" };
    return (
        <div className="content-center justify-center my-2">
            <GoogleLogin
                clientId={data_env.GOOGLE_CLIENT_ID}
                buttonText="Google Login"
                render={(renderProps) => (
                    <button onClick={renderProps.onClick} style={styles}>
                        <div className="google-btn border-rounded">
                            <div className="google-icon-wrapper border-rounded">
                                <img
                                    className="google-icon"
                                    src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg"
                                />
                            </div>
                            <p className="btn-text border-rounded text-xl">
                                Sign in with google
                            </p>
                        </div>
                    </button>
                )}
                onSuccess={googleResponse}
                onFailure={googleResponse}
                style={styles}
            />
        </div>
    );
};

export default LoginButton;
