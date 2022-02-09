import { combineReducers } from "redux";
import userReducer from "./user";
import socketReducer from "./socket";

const rootReducer = combineReducers({
    user: userReducer,
    socket: socketReducer,
});

export default rootReducer;
