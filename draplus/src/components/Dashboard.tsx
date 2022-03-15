import * as React from "react";
import "styles/Dashboard.css"
import Icon from "./Icon"
import { ResponsiveBar } from '@nivo/bar'
import { ResponsiveLine } from '@nivo/line'

interface DashboardProps {}

const Dashboard: React.FC<DashboardProps> = () => {

    const [data,setData] = React.useState(
        [
            {
                "country": "Mon",
                "login": 91,
                "loginColor": "hsl(205, 70%, 50%)"
            },
            {
                "country": "Tue",
                "login": 32,
                "loginColor": "hsl(205, 70%, 50%)"
            },
            {
                "country": "Wed",
                "login": 54,
                "loginColor": "hsl(205, 70%, 50%)"
            },
            {
                "country": "Thu",
                "login": 32,
                "loginColor": "hsl(205, 70%, 50%)"
            },
            {
                "country": "Fri",
                "login": 15,
                "loginColor": "hsl(205, 70%, 50%)"
            },
            {
                "country": "Sun",
                "login": 68,
                "loginColor": "hsl(205, 70%, 50%)"
            }
        ]
    ) 
    const [lineData,setLineData] = React.useState(
        [
            {
              "id": "japan",
              "color": "hsl(105, 70%, 50%)",
              "data": [
                {
                  "x": "plane",
                  "y": 97
                },
                {
                  "x": "helicopter",
                  "y": 197
                },
                {
                  "x": "boat",
                  "y": 253
                },
                {
                  "x": "train",
                  "y": 29
                },
                {
                  "x": "subway",
                  "y": 150
                },
                {
                  "x": "bus",
                  "y": 206
                },
                {
                  "x": "car",
                  "y": 22
                },
                {
                  "x": "moto",
                  "y": 68
                },
                {
                  "x": "bicycle",
                  "y": 182
                },
                {
                  "x": "horse",
                  "y": 232
                },
                {
                  "x": "skateboard",
                  "y": 92
                },
                {
                  "x": "others",
                  "y": 44
                }
              ]
            },
            {
              "id": "france",
              "color": "hsl(272, 70%, 50%)",
              "data": [
                {
                  "x": "plane",
                  "y": 267
                },
                {
                  "x": "helicopter",
                  "y": 0
                },
                {
                  "x": "boat",
                  "y": 289
                },
                {
                  "x": "train",
                  "y": 61
                },
                {
                  "x": "subway",
                  "y": 99
                },
                {
                  "x": "bus",
                  "y": 146
                },
                {
                  "x": "car",
                  "y": 11
                },
                {
                  "x": "moto",
                  "y": 110
                },
                {
                  "x": "bicycle",
                  "y": 223
                },
                {
                  "x": "horse",
                  "y": 286
                },
                {
                  "x": "skateboard",
                  "y": 206
                },
                {
                  "x": "others",
                  "y": 154
                }
              ]
            },
            {
              "id": "us",
              "color": "hsl(359, 70%, 50%)",
              "data": [
                {
                  "x": "plane",
                  "y": 261
                },
                {
                  "x": "helicopter",
                  "y": 241
                },
                {
                  "x": "boat",
                  "y": 251
                },
                {
                  "x": "train",
                  "y": 63
                },
                {
                  "x": "subway",
                  "y": 210
                },
                {
                  "x": "bus",
                  "y": 250
                },
                {
                  "x": "car",
                  "y": 160
                },
                {
                  "x": "moto",
                  "y": 27
                },
                {
                  "x": "bicycle",
                  "y": 118
                },
                {
                  "x": "horse",
                  "y": 77
                },
                {
                  "x": "skateboard",
                  "y": 182
                },
                {
                  "x": "others",
                  "y": 225
                }
              ]
            },
            {
              "id": "germany",
              "color": "hsl(122, 70%, 50%)",
              "data": [
                {
                  "x": "plane",
                  "y": 159
                },
                {
                  "x": "helicopter",
                  "y": 222
                },
                {
                  "x": "boat",
                  "y": 273
                },
                {
                  "x": "train",
                  "y": 68
                },
                {
                  "x": "subway",
                  "y": 153
                },
                {
                  "x": "bus",
                  "y": 169
                },
                {
                  "x": "car",
                  "y": 216
                },
                {
                  "x": "moto",
                  "y": 60
                },
                {
                  "x": "bicycle",
                  "y": 27
                },
                {
                  "x": "horse",
                  "y": 67
                },
                {
                  "x": "skateboard",
                  "y": 186
                },
                {
                  "x": "others",
                  "y": 249
                }
              ]
            },
            {
              "id": "norway",
              "color": "hsl(223, 70%, 50%)",
              "data": [
                {
                  "x": "plane",
                  "y": 86
                },
                {
                  "x": "helicopter",
                  "y": 53
                },
                {
                  "x": "boat",
                  "y": 242
                },
                {
                  "x": "train",
                  "y": 81
                },
                {
                  "x": "subway",
                  "y": 178
                },
                {
                  "x": "bus",
                  "y": 5
                },
                {
                  "x": "car",
                  "y": 196
                },
                {
                  "x": "moto",
                  "y": 164
                },
                {
                  "x": "bicycle",
                  "y": 224
                },
                {
                  "x": "horse",
                  "y": 58
                },
                {
                  "x": "skateboard",
                  "y": 0
                },
                {
                  "x": "others",
                  "y": 140
                }
              ]
            }
          ]
    );

    return(
        <>
            <div className="dashboard w-full h-[80%]">
                <div className="mx-[2rem]">
                <div className="dashboard-title">
                        Dashboard
                    </div>
                    <div className="dashboard-sub">
                            Admin {'>'} Dashboard
                    </div>
                    <div className="grid grid-flow-row sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mt-[2rem] ">
                        <div className=" dashboard-info grid grid-cols-1" style={{background: "#28A5F9"}}>
                            <div className="icon-contain ">
                                <Icon icon="user" className="m-3"/>
                            </div>
                            <text className=" info-contain">10</text>
                            <text className=" info-detail">New Account</text>
                        </div>
                        <div className=" dashboard-info grid grid-cols-1" style={{background: "#EF6F9E"}}>
                            <div className="icon-contain">
                                <Icon icon="chalkboard" className="m-3"/>
                            </div>
                            <text className=" info-contain">10</text>
                            <text className=" info-detail">New Board</text>
                        </div>
                        <div className=" dashboard-info grid grid-cols-1" style={{background: "#FAC66D"}}>
                            <div className="icon-contain">
                                <Icon icon="users" className="m-3"/>
                            </div>
                            <text className=" info-contain">10</text>
                            <text className=" info-detail">Total Accounts</text>
                        </div>
                        <div className=" dashboard-info grid grid-cols-1" style={{background: "#705DBC"}}>
                            <div className="icon-contain">
                                <Icon icon="clipboard" className="m-3"/>
                            </div>
                            <text className=" info-contain">10</text>
                            <text className=" info-detail">Total Boards</text>
                        </div>
                    </div>
                    <div className="grid grid-flow-row md:grid-cols-1 lg:grid-cols-2 gap-4 mt-[2rem]">
                        <div className="dashboard-chart ">
                            <ResponsiveBar
                                data={data}
                                keys={[
                                    'login'
                                ]}
                                indexBy="country"
                                margin={{ top: 50, right: 50, bottom: 50, left: 60 }}
                                padding={0.4}
                                valueScale={{ type: 'linear' }}
                                indexScale={{ type: 'band', round: true }}
                                colors={{ scheme: 'nivo' }}
                                defs={[
                                    {
                                        id: 'dots',
                                        type: 'patternDots',
                                        background: 'inherit',
                                        color: '#38bcb2',
                                        size: 4,
                                        padding: 1,
                                        stagger: true
                                    },
                                    {
                                        id: 'lines',
                                        type: 'patternLines',
                                        background: 'inherit',
                                        color: '#eed312',
                                        rotation: -45,
                                        lineWidth: 6,
                                        spacing: 10
                                    }
                                ]}
                                borderRadius={4}
                                borderColor={{
                                    from: 'color',
                                    modifiers: [
                                        [
                                            'darker',
                                            1.2
                                        ]
                                    ]
                                }}
                                barAriaLabel={function(e){return e.id+": "+e.formattedValue+" in country: "+e.indexValue}}
                            />
                        </div>
                        <div className="dashboard-chart ">
                            <ResponsiveLine
                                data={lineData}
                                margin={{ top: 50, right: 110, bottom: 50, left: 60 }}
                                xScale={{ type: 'point' }}
                                yScale={{
                                    type: 'linear',
                                    min: 'auto',
                                    max: 'auto',
                                    stacked: true,
                                    reverse: false
                                }}
                                yFormat=" >-.2f"
                                curve="basis"
                                colors={{ scheme: 'nivo' }}
                                lineWidth={6}
                                enablePoints={false}
                                pointColor={{ theme: 'background' }}
                                pointBorderWidth={2}
                                pointBorderColor={{ from: 'serieColor' }}
                                pointLabelYOffset={-12}
                                useMesh={true}
                                legends={[]}
                            />
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Dashboard;