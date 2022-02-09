import * as React from "react";
import { default as config } from "./config/config.json";
// import * as data from "./config/config.json";
import { GoogleLogin } from "react-google-login";

interface LoginButtonProps {}

const LoginButton: React.FC<LoginButtonProps> = () => {
    const googleResponse = () => {
        // console.log(response);
        // if (!response.tokenId) {
        //     console.error("Unable to get tokenId from Google", response);
        //     return;
        // }
        // const tokenBlob = new Blob(
        //     [JSON.stringify({ tokenId: response.tokenId }, null, 2)],
        //     { type: "application/json" }
        // );
        // const options = {
        //     method: "POST",
        //     body: tokenBlob,
        //     mode: "cors",
        //     cache: "default",
        // };
        // fetch(config.GOOGLE_AUTH_CALLBACK_URL, options)
        //     .then((r) => {
        //         r.json().then((user) => {
        //             const token = user.token;
        //             console.log(token);
        //             this.props.login(token);
        //         });
        //     })
        //     .catch((e) => {
        //         console.error(e);
        //     });
    };
    return (
        <div>
            <GoogleLogin
                clientId={config.GOOGLE_CLIENT_ID}
                buttonText="Google Login"
                onSuccess={googleResponse}
                onFailure={googleResponse}
            />
        </div>
    );
};

export default LoginButton;
