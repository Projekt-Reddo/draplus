import React from "react";

const Modal: React.FC<any> = ({ toggle }) => {
    return (
        <div
            className="h-2/4 w-1/4 flex flex-col items-center justify-center border-rounded app-shadow text-center"
            style={{ backgroundColor: "var(--element-bg)" }}
        >
            <h1 className="text-xl mb-4 font-bold text-white">Login Failed</h1>
            <p className="mb-4 text-white">Please try again</p>
            <button
                onClick={toggle}
                className="bg-red-500 px-7 py-2 ml-2 rounded-md text-md text-white font-semibold"
            >
                Ok
            </button>
        </div>
    );
};

export default Modal;
