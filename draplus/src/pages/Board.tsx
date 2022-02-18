// Libs
import * as React from "react";
import "styles/Board.css";

// Components
import CanvasBoard from "components/CanvasBoard";
import Chat from "components/Chat";
import Setting from "components/Setting";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    return (
        <>
            <CanvasBoard />
            <Chat />
            <Setting />
        </>
    );
};

export default Board;
