import { combineReducers } from "redux";
import userReducer from "./user";
import lcReducer from "./lc";
import shapeReducer from "./shape";
<<<<<<< HEAD
=======
import chatReducer from "./chat";
>>>>>>> c54afd7ba6bed98a6d85a863109f027e14f1f39c

const rootReducer = combineReducers({
    user: userReducer,
    initLC: lcReducer,
    shape: shapeReducer,
<<<<<<< HEAD
=======
    chat: chatReducer,
>>>>>>> c54afd7ba6bed98a6d85a863109f027e14f1f39c
});

export default rootReducer;
