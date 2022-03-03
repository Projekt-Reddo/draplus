import { OtherTool, Pencil, Text, Eraser } from "utils/constant";

const tool = Pencil;

const tools = [Pencil, OtherTool, Text, Eraser];

const toolReducer = (state = tool, action: ActionType) => {
    if (!tools.includes(action.type)) return state;

    switch (action.type) {
        case action.type:
            return action.type;

        default:
            return state;
    }
};

export default toolReducer;
