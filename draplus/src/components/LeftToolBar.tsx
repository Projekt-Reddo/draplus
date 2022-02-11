// Libs
import * as React from "react";
import { useState, useRef, useEffect } from "react";
import "styles/LeftToolBar.css";

// Components
import Icon from "components/Icon";

interface LeftToolBarProps {
    handleUndo: () => void;
    handleRedo: () => void;
    handleSelectColor: (colorCode: string) => void;
}

const doNothing = () => {};

const LeftToolBar: React.FC<LeftToolBarProps> = ({
    handleUndo,
    handleRedo,
    handleSelectColor,
}) => {
    const [showBrushOption, setShowBrushOption] = useState(false);
    const [isSelect, setIsSelect] = useState(1);
    const [colorSelect, setColorSelect] = useState("#fff");

    // State click outside
    const wrapperRef = useRef(null);
    useOutsideAlerter(wrapperRef, setShowBrushOption);

    const toolbars = [
        // { iconName: "pen", toolbarFunc: doNothing },
        { id: 2, iconName: "eraser", toolbarFunc: doNothing },
        { id: 3, iconName: "font", toolbarFunc: doNothing },
        { id: 4, iconName: "sticky-note", toolbarFunc: doNothing },
        { id: 5, iconName: "undo", toolbarFunc: handleUndo },
        { id: 6, iconName: "redo", toolbarFunc: handleRedo },
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

    const handleSelect = (codeButton: number) => {
        setIsSelect(codeButton);
    };

    return (
        <div>
            <div className="leftToolBar absolute grid grid-cols-1 gap-3 overflow-y-hidden content-center h-5/6 w-12 z-10">
                {/* Brush */}
                <div
                    className="icon flex"
                    style={{ color: colorSelect }}
                    onClick={() => handleSelect(1)}
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
                        onClick={() => {
                            toolbar.toolbarFunc();
                            handleSelect(toolbar.id);
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
                        ? "brushOptionBoard absolute justify-center flex h-44 w-52 z-10"
                        : "brushOptionBoardHide"
                }`}
                ref={wrapperRef}
            >
                {/* Arrow */}
                <div className="grid grid-cols-1 content-center">
                    <div className="arrow-left" />
                </div>
                {/* Stroke Options */}
                <div className="grid grid-cols-1 content-center gap-4">
                    <div className="eyeXL" />
                    <div className="eyeL" />
                    <div className="eyeM" />
                    <div className="eyeS" />
                </div>
                <div className="py-4 mx-4">
                    <div className="verticalLine" />
                </div>
                {/* Color Options */}
                <div className="grid grid-cols-3 content-center gap-4">
                    {colors.map((color) => (
                        <div
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
