// Libs
import * as React from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router-dom";

// Store
import { JOIN_ROOM, LEAVE_ROOM } from "store/actions";

// Components
import CanvasBoard from "components/CanvasBoard";
import Chat from "components/Chat";
import Setting from "components/Setting";

// Styles
import "styles/Board.css";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    const dispatch = useDispatch();
    const params: any = useParams();

    React.useEffect(() => {
        if (params.boardId) {
            dispatch({
                type: JOIN_ROOM,
                payload: params.boardId,
            });
        }

        return () => {
            dispatch({
                type: LEAVE_ROOM,
                payload: null,
            });
        };
    }, []);

    return (
        <>
            <CanvasBoard />
            <Chat />
            <Setting />
        </>
    );
};

export default Board;
