import * as React from "react";
import "styles/DashboardOption.css"
import Icon from "./Icon"

interface DashboardOptionProps {}

const DashboardOption: React.FC<DashboardOptionProps> = () => {
    


    return(
        <>
        <div className=" inset-y-0 left-0 max-w-full min-h-screen dashboard-item invisible lg:visible ">
            <div className="fixed ml-[3rem]">
                <img src="/favicon.ico" className="fav-icon mt-[2rem]"></img>
                <div className="option-title flex justify-center items-center " >Draplus</div>
                <div className="option-option flex  items-center mt-[4rem]"><Icon icon="tachometer" className="m-3"/>Dasboard</div>
                <div className="option-option flex  items-center mt-[1rem]"><Icon icon="user-cog" className="m-3"/>User</div>   
            </div>
        </div>
        </>
    )
}
export default DashboardOption;