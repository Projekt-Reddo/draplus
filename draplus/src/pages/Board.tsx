// Libs
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";

// Store
import { JOIN_ROOM } from "store/actions";

// Components
import CanvasBoard from "components/CanvasBoard";
import Chat from "components/Chat";
import Setting from "components/Setting";

// Styles
import "styles/Board.css";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    // Lib State
    const dispatch = useDispatch();
    const params: any = useParams();

    // Global State
    const user = useSelector((state: any) => state.user);

    // Join Board
    React.useEffect(() => {
        if (params.boardId && user) {
            dispatch({
                type: JOIN_ROOM,
                payload: {
                    user: user.user.id,
                    board: params.boardId,
                },
            });
        }
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
