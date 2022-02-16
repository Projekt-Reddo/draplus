import { RECEIVE_SHAPE } from "store/actions";

const shapes = {};

const shapeReducer = (state = shapes, action: ActionType) => {
    switch (action.type) {
        case RECEIVE_SHAPE:
            return action.payload;
        default:
            return state;
    }
};

export default shapeReducer;
