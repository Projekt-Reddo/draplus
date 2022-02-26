// Libs
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";

// Store
import {
    JOIN_ROOM,
    LEAVE_ROOM,
    GET_ONLINE_USERS,
    ONLINE_USERS,
} from "store/actions";

// Components
import CanvasBoard from "components/CanvasBoard";
import Chat from "components/Chat";
import Setting from "components/Setting";
import Avatar from "components/Avatar";

// Styles
import "styles/Board.css";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    // Lib State
    const dispatch = useDispatch();
    const params: any = useParams();

    // Global State
    const user = useSelector((state: any) => state.user);
    const board = useSelector((state: any) => state.board);

    React.useEffect(() => {
        // Join Board
        if (params.boardId && user.user) {
            dispatch({
                type: JOIN_ROOM,
                payload: {
                    user: user.user,
                    board: params.boardId,
                },
            });
        }

        // User Leave Room
        return () => {
            dispatch({
                type: LEAVE_ROOM,
                payload: null,
            });
            dispatch({
                type: ONLINE_USERS,
                payload: [],
            });
        };
    }, []);

    // Get List User Online
    React.useEffect(() => {
        if (board) {
            dispatch({
                type: GET_ONLINE_USERS,
                payload: board,
            });
        }
    }, [board]);

    return (
        <>
            <CanvasBoard />
            <Chat />
            <Setting />
            <Avatar />
        </>
    );
};

export default Board;
