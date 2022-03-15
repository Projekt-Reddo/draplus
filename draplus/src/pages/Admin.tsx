import * as React from "react";

import TitleAdmin from "components/TitleAdmin";
import DashboardOption from "components/DashboardOption";
import Dashboard from "components/Dashboard";

interface AdminProps {}

const Admin: React.FC<AdminProps> = () => {
    return(
        <>
        <div className="w-full h-screen bg-[color:#171615]">
            <div className="flex">
                <DashboardOption/>
                <div className="grid w-full">
                    <TitleAdmin/>                    
                    <Dashboard/>
                </div>
            </div>
        </div>
        </>
    )
}

export default Admin;

