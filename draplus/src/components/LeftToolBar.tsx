// Libs
import * as React from "react";
import { RootStateOrAny, useDispatch, useSelector } from "react-redux";
import LC from "literallycanvas";

// Components
import Icon from "components/Icon";

//Style
import "styles/LeftToolBar.css";
import { OtherTool, Pencil } from "utils/constant";

//Store
import { CLEAR_ALL, DRAW_SHAPE, REDO, UNDO } from "store/actions";

interface LeftToolBarProps {}

const doNothing = () => {};

var watingClick: any = null;
var lastClick = 0;

const LeftToolBar: React.FC<LeftToolBarProps> = () => {
    // Global State
    const initLC = useSelector((state: RootStateOrAny) => state.initLC);
    const dispatch = useDispatch();
    const clear = useSelector((state: RootStateOrAny) => state.clear);

    // Handle State
    const [showBrushOption, setShowBrushOption] = React.useState(false);
    const [clearAllOption, setClearrAllOption] = React.useState(false);
    const [isSelect, setIsSelect] = React.useState(1);
    const [colorSelect, setColorSelect] = React.useState("#fff");
    const [strokeWidthSelect, setStrokeWidthSelect] = React.useState(5);

    // State click outside
    const wrapperRef = React.useRef(null);
    useOutsideAlerter(wrapperRef, setShowBrushOption);

    const wrapperRef2 = React.useRef(null);
    useOutsideAlerter2(wrapperRef2, setClearrAllOption);

    // Funtions Handle Draw Canvas
    // Select Tool
    const handleSelectTool = (toolName: string) => {
        initLC.setTool(new LC.tools[toolName](initLC));

        dispatch({
            type: toolName,
        });

        if (toolName === "Pencil") {
            initLC.tool.strokeWidth = strokeWidthSelect;
        }
        if (toolName === "Eraser") {
            initLC.tool.strokeWidth = 30;
        }
    };

    // Select stroke width for Brush
    const handleSelectToolStrokeWidth = (strokeWidth: number) => {
        setStrokeWidthSelect(strokeWidth);
        initLC.tool.strokeWidth = strokeWidth;
    };

    // Select color for Brush and Text tool
    const handleSelectToolColor = (colorCode: string) => {
        initLC.setColor("primary", colorCode);
    };

    // Undo canvas
    const handleUndo = () => {
        dispatch({
            type: UNDO,
        });
    };

    // Redo canvas
    const handleRedo = () => {
        dispatch({
            type: REDO,
        });
    };

    // Clear canvas
    const handleClear = (lc: any) => {
        initLC.clear();
        const lcShapeContainer = lc.getSnapshot(["shapes"]);
        dispatch({
            type: CLEAR_ALL,
        });
    };

    React.useEffect(() => {
        //handleClear(initLC);
    }, [clear]);

    // Handle active Button was selected
    const handleActiveButtonSelect = (buttonCode: number) => {
        if (buttonCode === 5 || buttonCode == 6) {
            return;
        }
        setIsSelect(buttonCode);
    };

    // Const Variable
    const toolbars = [
        // {
        //     id: 2,
        //     iconName: "eraser",
        //     toolbarFunc: doNothing,
        //     toolName: "Eraser",
        // },
        { id: 3, iconName: "font", toolbarFunc: doNothing, toolName: "Text" },
        {
            id: 4,
            iconName: "sticky-note",
            toolbarFunc: doNothing,
            toolName: OtherTool,
        },
        { id: 5, iconName: "undo", toolbarFunc: handleUndo, toolName: "" },
        { id: 6, iconName: "redo", toolbarFunc: handleRedo, toolName: "" },
    ];

    const colors = [
        "#E61225",
        "#AB238A",
        "#D30079",
        "#F6620D",
        "#5A2C90",
        "#C19E67",
        "#FFC015",
        "#0168BF",
        "#B6B7B6",
        "#03A557",
        "#33CDFF",
        "#fff",
    ];

    const strokes = [
        { width: 9, size: "eyeXL" },
        { width: 5, size: "eyeL" },
        { width: 3, size: "eyeM" },
        { width: 2, size: "eyeS" },
    ];

    return (
        <>
            <div className="app-shadow leftToolBar absolute grid grid-cols-1 gap-5 overflow-y-hidden content-center h-[33rem] w-14 z-10">
                {/* Tools */}
                {/* Brush */}
                <div
                    className="icon flex"
                    style={{ color: colorSelect }}
                    onClick={(e) => {
                        if (
                            lastClick &&
                            e.timeStamp - lastClick < 250 &&
                            watingClick
                        ) {
                            lastClick = 0;
                            clearTimeout(watingClick);
                            setShowBrushOption(!showBrushOption);
                            watingClick = null;
                        } else {
                            lastClick = e.timeStamp;
                            watingClick = setTimeout(() => {
                                watingClick = null;
                                handleActiveButtonSelect(1);
                                handleSelectTool(Pencil);
                            }, 251);
                        }
                    }}
                >
                    <div
                        className={` ${isSelect === 1 ? "whiteLine" : "line"}`}
                    />
                    <div className="text-center self-center w-full">
                        <Icon icon="pen" style={{ fontSize: "1.5rem" }} />
                    </div>
                </div>
                {/* Eraser */}
                <div
                    className="icon flex"
                    onClick={(e) => {
                        if (
                            lastClick &&
                            e.timeStamp - lastClick < 250 &&
                            watingClick
                        ) {
                            lastClick = 0;
                            clearTimeout(watingClick);
                            // handle show clear all button
                            setClearrAllOption(!clearAllOption);
                            watingClick = null;
                        } else {
                            lastClick = e.timeStamp;
                            watingClick = setTimeout(() => {
                                watingClick = null;
                                handleActiveButtonSelect(2);
                                handleSelectTool("Eraser");
                            }, 251);
                        }
                    }}
                >
                    <div
                        className={` ${isSelect === 2 ? "whiteLine" : "line"}`}
                    />
                    <div className="text-center self-center w-full">
                        <Icon icon="eraser" style={{ fontSize: "1.5rem" }} />
                    </div>
                </div>
                {/* Eraser, Text, Note, Undo, Redo */}
                {toolbars.map((toolbar) => (
                    <div
                        key={toolbar.id}
                        className="icon flex"
                        style={
                            toolbar.iconName === "font"
                                ? { color: colorSelect }
                                : {}
                        }
                        onClick={() => {
                            toolbar.toolbarFunc();
                            handleActiveButtonSelect(toolbar.id);
                            if (toolbar.toolName !== "") {
                                handleSelectTool(toolbar.toolName);
                            }
                        }}
                    >
                        <div
                            className={` ${
                                isSelect === toolbar.id ? "whiteLine" : "line"
                            }`}
                        />
                        <div className="text-center self-center w-full">
                            <Icon
                                icon={toolbar.iconName}
                                style={{ fontSize: "1.5rem" }}
                            />
                        </div>
                    </div>
                ))}
            </div>
            {/* Brush Option Board */}
            <div
                className={` ${
                    showBrushOption
                        ? "app-shadow brushOptionBoard absolute justify-center flex h-44 w-52 z-10"
                        : "brushOptionBoardHide"
                }`}
                ref={wrapperRef}
            >
                {/* Stroke Options */}
                <div className="grid grid-cols-1 content-center gap-4">
                    {strokes.map((stroke) => (
                        <div
                            key={stroke.width}
                            className={stroke.size}
                            onClick={() =>
                                handleSelectToolStrokeWidth(stroke.width)
                            }
                        />
                    ))}
                </div>
                {/* Vertiacal White Line */}
                <div className="py-4 mx-4">
                    <div className="verticalLine" />
                </div>
                {/* Color Options */}
                <div className="grid grid-cols-3 content-center gap-4">
                    {colors.map((color) => (
                        <div
                            key={color}
                            style={{ backgroundColor: color }}
                            className="dot"
                            onClick={() => {
                                handleSelectToolColor(color);
                                setColorSelect(color);
                            }}
                        />
                    ))}
                </div>
            </div>
            {/* Clear all Option Board */}
            <div
                className={` ${
                    clearAllOption
                        ? "app-shadow clearAllBoard absolute justify-center flex h-44 w-52 z-10"
                        : "clearAllBoardHide"
                }`}
                ref={wrapperRef2}
            >
                <div className="text-center self-center w-full">
                    <Icon
                        icon="trash"
                        style={{ fontSize: "1.5rem" }}
                        onClick={() => {
                            handleClear(initLC);
                        }}
                    />
                </div>
            </div>
        </>
    );
};

export default LeftToolBar;

// Handle click outside
export function useOutsideAlerter(ref: any, setShowBrushOption: any) {
    React.useEffect(() => {
        /**
         * Set listData to null
         */
        function handleClickOutside(event: any) {
            if (ref.current && !ref.current.contains(event.target)) {
                setShowBrushOption(false);
            }
        }

        // Bind the event listener
        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            // Unbind the event listener on clean up
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [ref]);
}
// Handle click outside
export function useOutsideAlerter2(ref: any, setClearrAllOption: any) {
    React.useEffect(() => {
        /**
         * Set listData to null
         */
        function handleClickOutside(event: any) {
            if (ref.current && !ref.current.contains(event.target)) {
                setClearrAllOption(false);
            }
        }

        // Bind the event listener
        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            // Unbind the event listener on clean up
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [ref]);
}
