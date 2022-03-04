import { CONNECT_SIGNALR } from "store/actions";

const connection = {};

const connectionReducer = (state = connection, action: ActionType) => {
    switch (action.type) {
        case CONNECT_SIGNALR:
            return action.payload;

        default:
            return state;
    }
};

export default connectionReducer;
