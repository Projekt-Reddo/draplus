import { combineReducers } from "redux";
import userReducer from "./user";
import lcReducer from "./lc";
import shapeReducer from "./shape";
import chatReducer from "./chat";

const rootReducer = combineReducers({
    user: userReducer,
    initLC: lcReducer,
    shape: shapeReducer,
    chat: chatReducer,
});

export default rootReducer;
