import * as React from "react";
import { Navigate, useLocation } from "react-router-dom";

interface BannedPageProps {}

const BannedPage: React.FC<BannedPageProps> = () => {
    const location: any = useLocation();

    if (!location.state?.isBanned) {
        return <Navigate to="/" />;
    }

    return (
        <div
            className="h-screen w-screen flex items-center justify-center "
            style={{ backgroundColor: "var(--bg)" }}
        >
            <div
                className="h-1/2 w-1/4 flex flex-col items-center justify-center border-rounded app-shadow"
                style={{
                    backgroundColor: "var(--element-bg)",
                    minWidth: "350px",
                    maxWidth: "500px",
                }}
            >
                <div className="h-auto my-14 mx-2 content-center text-center flex flex-col justify-between items-center">
                    <p className="text-5xl text-red-600 font-bold">
                        You are banned
                    </p>
                </div>
                <div className="mx-2 mt-14 content-center text-center">
                    <p className="text-2xl text-white">
                        Please contact our team at <br />
                        <a href="mailto:draplus.info@gmail.com?subject=Please unban user">
                            draplus.info@gmail.com
                        </a>
                    </p>
                </div>
            </div>
        </div>
    );
};

export default BannedPage;
