import { NEW_SOCKET } from "../actions";

const socket = null;

const socketReducer = (state = socket, action: ActionType) => {
    switch (action.type) {
        case NEW_SOCKET: {
            return action.payload;
        }

        default:
            return state;
    }
};

export default socketReducer;
