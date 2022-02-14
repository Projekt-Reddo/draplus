import * as React from "react";
import { default as data } from "../config/config.json";
import {
    GoogleLogin,
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";
import { useDispatch } from "react-redux";
import { login } from "store/actions/index";
import "styles/GoogleLoginButton.css";

interface LoginButtonProps {}
interface IAuthResponse {
    token: string;
}

const LoginButton: React.FC<LoginButtonProps> = () => {
    // For dispatch redux
    const dispatch = useDispatch();

    const googleResponse = (
        response: GoogleLoginResponse | GoogleLoginResponseOffline
    ) => {
        if (!(response as GoogleLoginResponse).tokenId) {
            console.error("Unable to get token from Google", response);
            return;
        }
        const tokenBlob = new Blob(
            [
                JSON.stringify(
                    { tokenId: (response as GoogleLoginResponse).tokenId },
                    null,
                    2
                ),
            ],
            { type: "application/json" }
        );
        const options: Object = {
            method: "POST",
            body: tokenBlob,
            mode: "cors",
            cache: "default",
        };
        fetch(data.GOOGLE_AUTH_CALLBACK_URL, options)
            .then((r) => {
                r.json().then((user) => {
                    dispatch(login(user));
                });
            })
            .catch((e) => {
                console.error(e);
            });
    };
    const styles = { borderRadius: 50, width: "100%" };
    return (
        <div className="content-center justify-center my-2">
            <GoogleLogin
                clientId={data.GOOGLE_CLIENT_ID}
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
                                <b>Sign in with google</b>
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
