// Libs
import * as React from "react";
import "styles/Board.css";

// Components
import CanvasBoard from "components/CanvasBoard";
import Chat from "components/Chat";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    return (
        <>
            <CanvasBoard />
            <Chat />
        </>
    );
};

export default Board;
