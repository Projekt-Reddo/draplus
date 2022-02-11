// Libs
import * as React from "react";
import "styles/Board.css";

// Components
import CanvasBoard from "components/CanvasBoard";

interface BoardProps {}

const Board: React.FC<BoardProps> = () => {
    return <CanvasBoard />;
};

export default Board;
