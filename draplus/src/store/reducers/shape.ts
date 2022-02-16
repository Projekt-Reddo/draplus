import { RECEIVE_SHAPE } from "store/actions";

<<<<<<< HEAD
const shapes = {};
=======
const shapes: object[] = [];
>>>>>>> c54afd7ba6bed98a6d85a863109f027e14f1f39c

const shapeReducer = (state = shapes, action: ActionType) => {
    switch (action.type) {
        case RECEIVE_SHAPE:
<<<<<<< HEAD
            return action.payload;
=======
            return [...state, action.payload.shape];
>>>>>>> c54afd7ba6bed98a6d85a863109f027e14f1f39c
        default:
            return state;
    }
};

export default shapeReducer;
