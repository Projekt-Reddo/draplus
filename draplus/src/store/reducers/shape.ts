import { RECEIVE_SHAPE, REMOVE_SHAPE } from "store/actions";

const shapes: object[] = [];

const shapeReducer = (state = shapes, action: ActionType) => {
    switch (action.type) {
        case RECEIVE_SHAPE:
            return [...state, action.payload];

        case REMOVE_SHAPE: {
            return state.filter((shape: any) => shape.id !== action.payload);
        }

        default:
            return state;
    }
};

export default shapeReducer;
