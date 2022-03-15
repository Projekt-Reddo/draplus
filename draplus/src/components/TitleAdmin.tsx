import * as React from "react";

import Avatar from "components/Avatar";
import Setting from "components/Setting";
import "styles/TitleAdmin.css";
import { RootStateOrAny, useSelector } from "react-redux";
import { Menu, Transition } from "@headlessui/react";


interface TitleAdminProps {}

const TitleAdmin: React.FC<TitleAdminProps> = () => {
    const user = useSelector((state: RootStateOrAny) => state.user)


    return(
        <>
            <div className="origin-top w-full title-item ">
                <text className="title-font">Welcome back {user.user.name} </text>
                <text className="title-sub">Below is admin view</text>
                <Avatar/>
                <Setting/>
            </div>
        </>
    )
}
export default TitleAdmin;