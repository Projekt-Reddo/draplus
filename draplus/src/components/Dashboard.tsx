import * as React from "react";
import "styles/Dashboard.css"
import Icon from "./Icon"

interface DashboardProps {}

const Dashboard: React.FC<DashboardProps> = () => {
    return(
        <>
            <div className="dashboard ">
                    <div className="dashboard-title">
                        Dashboard
                    </div>
                    <div className="dashboard-sub">
                            Admin {'>'} Dashboard
                    </div>
                    <div className="flex justify-evenly mt-[2rem]">
                        <div className="relative dashboard-info" style={{background: "#28A5F9"}}>
                            <div className="icon-contain ">
                                <Icon icon="user" className="m-3"/>
                            </div>
                            <text className="absolute bottom-[30px] left-[20px] info-contain">10</text>
                            <text className="absolute bottom-[20px] left-[20px] info-detail">New Account</text>
                        </div>
                        <div className="relative dashboard-info" style={{background: "#EF6F9E"}}>
                            <div className="icon-contain">
                                <Icon icon="chalkboard" className="m-3"/>
                            </div>
                            <text className="absolute bottom-[30px] left-[20px] info-contain">10</text>
                            <text className="absolute bottom-[20px] left-[20px] info-detail">New Board</text>
                        </div>
                        <div className="relative dashboard-info" style={{background: "#FAC66D"}}>
                            <div className="icon-contain">
                                <Icon icon="users" className="m-3"/>
                            </div>
                            <text className="absolute bottom-[30px] left-[20px] info-contain">10</text>
                            <text className="absolute bottom-[20px] left-[20px] info-detail">Total Accounts</text>
                        </div>
                        <div className="relative dashboard-info" style={{background: "#705DBC"}}>
                            <div className="icon-contain">
                                <Icon icon="clipboard" className="m-3"/>
                            </div>
                            <text className="absolute bottom-[30px] left-[20px] info-contain">10</text>
                            <text className="absolute bottom-[20px] left-[20px] info-detail">Total Boards</text>
                        </div>
                    </div>
                    <div className="flex justify-evenly mt-[2rem]">
                        <div className="dashboard-chart">A</div>
                        <div className="dashboard-chart">B</div>
                    </div>
            </div>
        </>
    )
}

export default Dashboard;