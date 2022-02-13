import { combineReducers } from "redux";
import userReducer from "./user";
import socketReducer from "./socket";
import lcReducer from "./lc";

const rootReducer = combineReducers({
    user: userReducer,
    socket: socketReducer,
    initLC: lcReducer,
});

export default rootReducer;
