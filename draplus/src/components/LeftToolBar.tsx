// Libs
import * as React from "react";
import { useState, useRef, useEffect } from "react";
import "styles/LeftToolBar.css";

// Components
import Icon from "components/Icon";

interface LeftToolBarProps {
    handleSelectTool: (toolName: string) => void;
    handleSelectStrokeWidth: (strokeWidth: Number) => void;
    handleSelectColor: (colorCode: string) => void;
    handleUndo: () => void;
    handleRedo: () => void;
}

const doNothing = () => {};

const LeftToolBar: React.FC<LeftToolBarProps> = ({
    handleSelectTool,
    handleSelectStrokeWidth,
    handleSelectColor,
    handleUndo,
    handleRedo,
}) => {
    const [showBrushOption, setShowBrushOption] = useState(false);
    const [isSelect, setIsSelect] = useState(1);
    const [colorSelect, setColorSelect] = useState("#fff");

    // State click outside
    const wrapperRef = useRef(null);
    useOutsideAlerter(wrapperRef, setShowBrushOption);

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

    // Handle select Tool
    const handleSelect = (codeButton: number) => {
        setIsSelect(codeButton);
    };

    return (
        <div>
            <div className="app-shadow leftToolBar absolute grid grid-cols-1 overflow-y-hidden content-center h-5/6 w-12 z-10">
                {/* Brush */}
                <div
                    className="icon flex"
                    style={{ color: colorSelect }}
                    onClick={() => {
                        handleSelect(1);
                        handleSelectTool("Pencil");
                    }}
                    onDoubleClick={() => {
                        setShowBrushOption(!showBrushOption);
                    }}
                >
                    <div
                        className={` ${isSelect === 1 ? "whiteLine" : "line"}`}
                    />
                    <div className="text-center self-center w-full">
                        <Icon icon="pen" style={{ fontSize: "1.5rem" }} />
                    </div>
                </div>
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
                            handleSelect(toolbar.id);
                            handleSelectTool(toolbar.toolName);
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
                                handleSelectStrokeWidth(stroke.width)
                            }
                        />
                    ))}
                </div>
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
                                handleSelectColor(color);
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
    useEffect(() => {
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
