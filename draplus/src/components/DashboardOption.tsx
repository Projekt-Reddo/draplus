import * as React from "react";
import "styles/DashboardOption.css"
import icon from "../../public/favicon.ico"

interface DashboardOptionProps {}

const DashboardOption: React.FC<DashboardOptionProps> = () => {
    
    return(
        <>
        <div className="absolute inset-y-0 left-0 h-screen  dashboard-item ">
            <img src="/favicon.ico" className="fav-icon mt-[2rem]"></img>
            <div className="option-title flex justify-center items-center " >Draplus</div>
            <div className="option-option flex justify-center items-center mt-[4rem]" >Dasboard</div>
            <div className="option-option flex justify-center items-center mt-[1rem]" >User</div>   
        </div>
        </>
    )
}
export default DashboardOption;