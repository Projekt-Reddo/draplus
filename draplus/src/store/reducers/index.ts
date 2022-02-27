import { combineReducers } from "redux";
import userReducer from "./user";
import lcReducer from "./lc";
import shapeReducer from "./shape";
import chatReducer from "./chat";
import mouseReducer from "./mouse";
import onlineUsersReducer from "./onlineUsers";
import boardReducer from "./board";
import toolReducer from "./tool";
import noteReducer from "./note";

const rootReducer = combineReducers({
    user: userReducer,
    initLC: lcReducer,
    shape: shapeReducer,
    chat: chatReducer,
    mouse: mouseReducer,
    onlineUsers: onlineUsersReducer,
    board: boardReducer,
    tool: toolReducer,
    notes: noteReducer,
});

export default rootReducer;
