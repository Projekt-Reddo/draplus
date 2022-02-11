<<<<<<< HEAD
const user = null;

const userReducer = (state = user, action: ActionType) => {
    switch (action.type) {
=======
const user = {
    user: "",
    isAuthenticated: false,
};

const userReducer = (state = user, action: ActionType) => {
    switch (action.type) {
        case "LOGIN":
            state = { ...state, user: action.payload, isAuthenticated: true };
            break;
        case "LOGOUT":
            state = { ...state, user: "", isAuthenticated: false };
            break;
>>>>>>> 537f4a7702f2759c1130634f9907b4486915f706
        default:
            return state;
    }
};

export default userReducer;
