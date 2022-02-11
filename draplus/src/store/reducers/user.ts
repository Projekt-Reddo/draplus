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
        default:
            return state;
    }
};

export default userReducer;
