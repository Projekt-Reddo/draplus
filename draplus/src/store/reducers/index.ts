import { combineReducers } from "redux";
import userReducer from "./user";
import lcReducer from "./lc";
import shapeReducer from "./shape";
import chatReducer from "./chat";
import mouseReducer from "./mouse";
import onlineUsersReducer from "./onlineUsers";

const rootReducer = combineReducers({
    user: userReducer,
    initLC: lcReducer,
    shape: shapeReducer,
    chat: chatReducer,
    mouse: mouseReducer,
    onlineUsers: onlineUsersReducer,
});

export default rootReducer;
