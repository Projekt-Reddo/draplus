import LoginWrapper from "components/LoginWrapper";
import * as React from "react";
import { default as data_env } from "../config/config.json";

import { login } from "store/actions/index";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useMutation } from "react-query";

import {
    GoogleLoginResponse,
    GoogleLoginResponseOffline,
} from "react-google-login";
import useModal from "utils/useModal";

interface LoginProps {}

const Login: React.FC<LoginProps> = () => {
    // for modal
    const { isShowing, toggle } = useModal();

    let navigate = useNavigate();
    // For dispatch redux
    const dispatch = useDispatch();

    // For fetching data from server
    const mutation = useMutation((optionsFetch: Object) => {
        return fetch(data_env.GOOGLE_AUTH_CALLBACK_URL, optionsFetch);
    });

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
        mutation.mutate(options, {
            onSuccess: (r: any) => {
                if (!r.ok) {
                    console.error(r);
                    toggle();
                    return;
                }
                r.json().then((user: any) => {
                    dispatch(login(user));
                    navigate(`/board`);
                });
            },
            onError: (e: any) => {
                console.error(e);
            },
        });
    };

    return (
        <div
            className="h-screen w-screen flex items-center justify-center"
            style={{ backgroundColor: "var(--bg)" }}
        >
            <LoginWrapper
                googleResponse={googleResponse}
                mutation={mutation}
                isShowing={isShowing}
                toggle={toggle}
            />
        </div>
    );
};

export default Login;
