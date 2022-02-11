// Libs
import * as React from "react";
import { useState } from "react";
import LC from "literallycanvas";
import "literallycanvas/lib/css/literallycanvas.css";
import "styles/CanvasBoard.css";

// Components
import LeftToolBar from "components/LeftToolBar";

interface CanvasBoardProps {}

const CanvasBoard: React.FC<CanvasBoardProps> = () => {
    const [init, setInit] = useState<typeof LC>();

    const handleDrawingChange = (lc: any) => {
        // const annotation = lc.getSnapshot(["shapes"]);
        // console.log(annotation);
    };

    const handleSelectColor = (colorCode: string) => {
        init.setColor("primary", colorCode);
    };

    const handleUndo = () => {
        init.undo();
    };

    const handleRedo = () => {
        init.redo();
    };

    const handleInit = (lc: any) => {
        setInit(lc);
        lc.on("drawingChange", () => handleDrawingChange(lc));
    };

    return (
        <div>
            <LeftToolBar
                handleUndo={handleUndo}
                handleRedo={handleRedo}
                handleSelectColor={handleSelectColor}
            />
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
