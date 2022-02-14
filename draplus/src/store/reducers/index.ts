import { combineReducers } from "redux";
import userReducer from "./user";
import lcReducer from "./lc";

const rootReducer = combineReducers({
    user: userReducer,
    initLC: lcReducer,
});

export default rootReducer;
