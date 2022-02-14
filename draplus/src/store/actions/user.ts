import { LOGIN } from ".";

export const login = (data: any) => {
    var accessToken = data.accessToken;
    localStorage.setItem("accessToken", accessToken);
    // filter object
    // const notAllowed = ["accessToken", "id"];
    // Object.keys(data)
    //     .filter((key) => notAllowed.includes(key))
    //     .forEach((key) => delete data[key]);
    delete data.accessToken;
    localStorage.setItem("user", JSON.stringify(data));

    return {
        type: LOGIN,
        payload: { ...data, accessToken: accessToken },
    };
    // return (dispath: any) => {
    //     dispath({
    //         type: "LOGIN",
    //         payload: token,
    //     });
    // };
};
