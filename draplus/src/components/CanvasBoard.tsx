// Libs
import * as React from "react";
import { RootStateOrAny, useDispatch, useSelector } from "react-redux";
import LC from "literallycanvas";

// Components
import LeftToolBar from "components/LeftToolBar";
import Cursor from "components/Cursor";

// Store
import { DRAW_SHAPE, INITLC, SEND_MOUSE } from "store/actions";

// Style
import "literallycanvas/lib/css/literallycanvas.css";
import "styles/CanvasBoard.css";

interface CanvasBoardProps {}

var timer: any;

const CanvasBoard: React.FC<CanvasBoardProps> = () => {
    // Redux state
    const dispatch = useDispatch();
    const shape = useSelector((state: RootStateOrAny) => state.shape);

    // Handle State
    const [localInitLC, setLocalInitLC] = React.useState<typeof LC>();
    const [myShape, setMyShape] = React.useState<object[]>([]);

    // Get Change of Canvas
    // Send user's shape to other user
    // Set user's shape to myShape state
    const handleDrawingChange = (lc: any, shape: any) => {
        const lcShapeContainer = lc.getSnapshot(["shapes"]);
        setMyShape(lcShapeContainer.shapes);
        dispatch({
            type: DRAW_SHAPE,
            payload:
                lcShapeContainer.shapes[lcShapeContainer.shapes.length - 1],
        });
    };

    // Create Init of Literally Canvas
    const handleInit = (lc: any) => {
        setLocalInitLC(lc);
        dispatch({ type: INITLC, payload: lc });
        lc.on("shapeSave", (shape: any) => handleDrawingChange(lc, shape));
    };

    // Load Shape of the other User
    React.useEffect(() => {
        if (localInitLC && shape !== []) {
            localInitLC.loadSnapshot({
                shapes: shape.concat(myShape),
            });
        }
    }, [shape]);

    const getMousePosition = (e: any) => {
        dispatch({
            type: SEND_MOUSE,
            payload: {
                x: e.pageX,
                y: e.pageY,
                isMove: true,
            },
        });
        clearTimeout(timer);
        timer = setTimeout(() => {
            dispatch({
                type: SEND_MOUSE,
                payload: {
                    x: e.pageX,
                    y: e.pageY,
                    isMove: false,
                },
            });
        }, 300);
    };

    return (
        <div onMouseMove={getMousePosition}>
            {/* Cursor */}
            <Cursor />
            {/* Left Toolbar */}
            <LeftToolBar />
            {/* Canvas Board */}
            <LC.LiterallyCanvasReactComponent
                onInit={handleInit}
                primaryColor="#fff"
                backgroundColor="#232222"
                toolbarPosition="hidden"
            />
        </div>
    );
};

export default CanvasBoard;
