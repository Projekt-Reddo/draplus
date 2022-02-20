// Libs
import * as React from "react";
import { RootStateOrAny, useSelector } from "react-redux";
import LC from "literallycanvas";

// Components
import Icon from "components/Icon";

//Style
import "styles/LeftToolBar.css";

interface LeftToolBarProps {}

const doNothing = () => {};

var watingClick: any = null;
var lastClick = 0;

const LeftToolBar: React.FC<LeftToolBarProps> = () => {
    // Global State
    const initLC = useSelector((state: RootStateOrAny) => state.initLC);

    // Handle State
    const [showBrushOption, setShowBrushOption] = React.useState(false);
    const [isSelect, setIsSelect] = React.useState(1);
    const [colorSelect, setColorSelect] = React.useState("#fff");

    // State click outside
    const wrapperRef = React.useRef(null);
    useOutsideAlerter(wrapperRef, setShowBrushOption);

    // Funtions Handle Draw Canvas
    // Select Tool
    const handleSelectTool = (toolName: string) => {
        initLC.setTool(new LC.tools[toolName](initLC));
        if (toolName === "Eraser") {
            handleSelectToolStrokeWidth(30);
        }
    };

    // Select stroke width for Brush
    const handleSelectToolStrokeWidth = (strokeWidth: Number) => {
        initLC.tool.strokeWidth = strokeWidth;
    };

    // Select color for Brush and Text tool
    const handleSelectToolColor = (colorCode: string) => {
        initLC.setColor("primary", colorCode);
    };

    // Undo canvas
    const handleUndo = () => {
        initLC.undo();
    };

    // Redo canvas
    const handleRedo = () => {
        initLC.redo();
    };

    // Handle active Button was selected
    const handleActiveButtonSelect = (buttonCode: number) => {
        if (buttonCode === 5 || buttonCode == 6) {
            return;
        }
        setIsSelect(buttonCode);
    };

    // Const Variable
    const toolbars = [
        {
            id: 2,
            iconName: "eraser",
            toolbarFunc: doNothing,
            toolName: "Eraser",
        },
        { id: 3, iconName: "font", toolbarFunc: doNothing, toolName: "Text" },
        {
            id: 4,
            iconName: "sticky-note",
            toolbarFunc: doNothing,
            toolName: "",
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
        <div>
            <div className="app-shadow leftToolBar absolute grid grid-cols-1 gap-5 overflow-y-hidden content-center h-5/6 w-14 z-10">
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
                                handleSelectTool("Pencil");
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
        </div>
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
