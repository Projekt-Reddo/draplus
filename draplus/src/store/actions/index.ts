export const NEW_SOCKET = "socket/new";
<<<<<<< HEAD
=======
export const LOGIN = "login";

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
>>>>>>> 537f4a7702f2759c1130634f9907b4486915f706
