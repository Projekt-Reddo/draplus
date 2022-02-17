// Libs
import * as React from "react";
import { RootStateOrAny, useDispatch, useSelector } from "react-redux";
import LC from "literallycanvas";

// Components
import LeftToolBar from "components/LeftToolBar";

// Store
import { DRAW_SHAPE, INITLC } from "store/actions";

// Style
import "literallycanvas/lib/css/literallycanvas.css";
import "styles/CanvasBoard.css";

interface CanvasBoardProps {}

const CanvasBoard: React.FC<CanvasBoardProps> = () => {
    // Redux state
    const dispatch = useDispatch();
    const user = useSelector((state: any) => state.user);
    const shape = useSelector((state: RootStateOrAny) => state.shape);

    console.log(user);
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
            payload: {
                user: user.user,
                lastShape:
                    lcShapeContainer.shapes[lcShapeContainer.shapes.length - 1],
            },
        });
    };

    // Create Init of Literally Canvas
    const handleInit = (lc: any) => {
        setLocalInitLC(lc);
        dispatch({ type: INITLC, payload: lc });
        lc.on("shapeSave", (shape: any) => handleDrawingChange(lc, shape));
    };

    React.useEffect(() => {
        if (localInitLC && shape !== []) {
            localInitLC.loadSnapshot({
                shapes: shape.concat(myShape),
            });
        }
    }, [shape]);

    return (
        <div>
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

// const shapes = {
//     shapes: [
//         {
//             className: "LinePath",
//             data: {
//                 order: 3,
//                 pointColor: "hsla(0, 0%, 0%, 1)",
//                 pointCoordinatePairs: [[199, 149.25]],
//                 pointSize: 5,
//                 smooth: true,
//                 smoothedPointCoordinatePairs: [
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                 ],
//                 tailSize: 3,
//             },
//             id: "277dc281-c10a-9b13-c7ef-47c501c471a9",
//         },
//     ],
// };

// const data = {
//     shapes: [
//         // Line Path
//         {
//             className: "LinePath",
//             data: {
//                 order: 3,
//                 pointColor: "hsla(0, 0%, 0%, 1)",
//                 pointCoordinatePairs: [[199, 149.25]],
//                 pointSize: 5,
//                 smooth: true,
//                 smoothedPointCoordinatePairs: [
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                 ],
//                 tailSize: 3,
//             },
//             id: "277dc281-c10a-9b13-c7ef-47c501c471a9",
//         },

//         // Erased Line Path
//         {
//             className: "ErasedLinePath",
//             data: {
//                 order: 3,
//                 pointColor: "#000",
//                 pointCoordinatePairs: [[199, 149.25]],
//                 pointSize: 5,
//                 smooth: true,
//                 smoothedPointCoordinatePairs: [
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                     [199, 149.25],
//                 ],
//                 tailSize: 3,
//             },
//             id: "52c41a32-427e-07ec-e63b-d48888d5af73",
//         },
//         // Text
//         {
//             className: "Text",
//             data: {
//                 color: "hsla(0, 0%, 0%, 1)",
//                 font: "18px",
//                 forcedHeight: 0,
//                 forcedWidth: 243,
//                 text: "dbdbd9",
//                 v: 1,
//                 x: 113,
//                 y: 89.25,
//             },
//             id: "924e9beb-58ff-aa02-b1f0-8ccee58eb112",
//         },
//     ],
// };
