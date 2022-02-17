import { RECEIVE_SHAPE } from "store/actions";

const shapes: object[] = [];

const shapeReducer = (state = shapes, action: ActionType) => {
    switch (action.type) {
        case RECEIVE_SHAPE:
            return [...state, action.payload];
        default:
            return state;
    }
};

export default shapeReducer;
