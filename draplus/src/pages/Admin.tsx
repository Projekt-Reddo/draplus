import * as React from "react";

import TitleAdmin from "components/TitleAdmin";
import DashboardOption from "components/DashboardOption";
import Dashboard from "components/Dashboard";

interface AdminProps {}

const Admin: React.FC<AdminProps> = () => {
    return(
        <>
        <TitleAdmin/>
        <div className="flex flex-row">
            <DashboardOption/>
            <Dashboard/>
        </div>
            
        </>
    )
}

export default Admin;

