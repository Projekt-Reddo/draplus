// Libs
import * as React from "react";
import { useState } from "react";
import LC from "literallycanvas";
import "literallycanvas/lib/css/literallycanvas.css";
import "styles/CanvasBoard.css";

// Components
import LeftToolBar from "components/LeftToolBar";
import { debug } from "console";

interface CanvasBoardProps {}

const CanvasBoard: React.FC<CanvasBoardProps> = () => {
    const [initLC, setInitLC] = useState<typeof LC>();

    const [data,setData] = useState();

    const handleDrawingChange = (lc: any) => {
        // const annotation = lc.getSnapshot(["shapes"]);
        // console.log(annotation);
    };

    const handleSelectStrokeWidth = (strokeWidth: Number) => {
        initLC.tool.strokeWidth = strokeWidth;
    };

    const handleSelectColor = (colorCode: string) => {
        initLC.setColor("primary", colorCode);
    };

    const handleSelectTool = (toolName: string) => {
        initLC.setTool(new LC.tools[toolName](initLC));
        if (toolName === "Eraser") {
            handleSelectStrokeWidth(30);
        }
    };

    const handleUndo = () => {
        initLC.undo();
    };

    const handleRedo = () => {
        initLC.redo();
    };

    const handleInit = (lc: any) => {
        setInitLC(lc);
        lc.on("drawingChange", () => handleDrawingChange(lc));
    };
    React.useEffect(() => {
        console.log(fetch(`https://localhost:7287/api/Board/620290017830b77933d595b8`).then(res=> res.json))
    }, [])

    

    return (
        <div>
            <LeftToolBar
                handleSelectTool={handleSelectTool}
                handleSelectStrokeWidth={handleSelectStrokeWidth}
                handleSelectColor={handleSelectColor}
                handleUndo={handleUndo}
                handleRedo={handleRedo}
            />
            <LC.LiterallyCanvasReactComponent
                onInit={handleInit}
                snapshot={data}
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
//         // {
//         //     className: "ErasedLinePath",
//         //     data: {
//         //         order: 3,
//         //         pointColor: "#000",
//         //         pointCoordinatePairs: [[199, 149.25]],
//         //         pointSize: 5,
//         //         smooth: true,
//         //         smoothedPointCoordinatePairs: [
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //             [199, 149.25],
//         //         ],
//         //         tailSize: 3,
//         //     },
//         //     id: "52c41a32-427e-07ec-e63b-d48888d5af73",
//         // },
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
