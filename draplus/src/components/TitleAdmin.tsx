import * as React from "react";

import Avatar from "components/Avatar";
import Setting from "components/Setting";
import "styles/TitleAdmin.css";
import { RootStateOrAny, useSelector } from "react-redux";


interface TitleAdminProps {}

const TitleAdmin: React.FC<TitleAdminProps> = () => {
    const user = useSelector((state: RootStateOrAny) => state.user)


    return(
        <>
        <div className=" inset-x-8 top-0 right-0 h-[6.6rem] title-item">
            <text className=" title-font">Welcome back {user.user.name} </text>
            <text className="title-sub">Below is admin view</text>
            <Avatar/>
            <Setting/>
        </div>
        </>
    )
}
export default TitleAdmin;