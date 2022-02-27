import { ADD_NOTE, DELETE_NOTE, UPDATE_NOTE } from "store/actions";

const note: Note[] = [];

const noteReducer = (state = note, action: ActionType) => {
    switch (action.type) {
        case ADD_NOTE:
            return [
                ...state,
                {
                    ...action.payload,
                    id: `${Date.now()} ${Math.random()}`,
                },
            ];

        case DELETE_NOTE:
            return state.filter((note) => note.id !== action.payload);

        case UPDATE_NOTE: {
            const { id, text } = action.payload;

            return state.map((note) => {
                if (note.id === id) {
                    return {
                        ...note,
                        text,
                    };
                }
                return note;
            });
        }

        default:
            return state;
    }
};

export default noteReducer;
