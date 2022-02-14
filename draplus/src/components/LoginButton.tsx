import * as React from "react";
import { default as data } from "../config/config.json";
import {
    GoogleLogin,
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";
import { useDispatch } from "react-redux";
import { login } from "store/actions/index";

import { useNavigate } from "react-router-dom";

interface LoginButtonProps {}
interface IAuthResponse {
    token: string;
}

const LoginButton: React.FC<LoginButtonProps> = () => {
    let navigate = useNavigate();
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
                    const token = user.accessToken;
                    dispatch(login(user));
                    navigate(`/${user.boardId}`);
                });
            })
            .catch((e) => {
                console.error(e);
            });
    };
    const styles = { borderRadius: 50 };
    return (
        <div className="content-center justify-center my-2">
            <GoogleLogin
                clientId={data.GOOGLE_CLIENT_ID}
                buttonText="Google Login"
                onSuccess={googleResponse}
                onFailure={googleResponse}
                style={styles}
            />
        </div>
    );
};

export default LoginButton;
