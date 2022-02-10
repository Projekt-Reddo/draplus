import * as React from "react";
import { default as data } from "../config/config.json";
import {
    GoogleLogin,
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";
import { useDispatch } from "react-redux";
import { login } from "store/actions/index";

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
        console.log(response);
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
        console.log(data.GOOGLE_AUTH_CALLBACK_URL);
        fetch(data.GOOGLE_AUTH_CALLBACK_URL, options)
            .then((r) => {
                r.json().then((user) => {
                    const token = user.accessToken;
                    dispatch(login(user));
                });
            })
            .catch((e) => {
                console.error(e);
            });
    };
    return (
        <div className="content-center justify-center my-2">
            <GoogleLogin
                clientId={data.GOOGLE_CLIENT_ID}
                buttonText="Google Login"
                onSuccess={googleResponse}
                onFailure={googleResponse}
            />
        </div>
    );
};

export default LoginButton;
