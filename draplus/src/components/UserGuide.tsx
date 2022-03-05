// Libs
import * as React from "react";

// Style
import "styles/UserGuide.css";

interface UserGuideProps {}

const mondais = [
    { id: 1, mondai: "What can I do with Dảkboard?" },
    { id: 2, mondai: "How do I start using Dảkboard on Draplus Web?" },
    { id: 3, mondai: "How do I invite other users to my DăkBoard?" },
    { id: 4, mondai: "How do I interact with Dảkboard?" },
    { id: 5, mondai: "How do I change brush width and brush color?" },
    { id: 6, mondai: "Why can't I see the Clear All button?" },
    { id: 7, mondai: "How do I create a new board?" },
    {
        id: 8,
        mondai: "How can I export a DảkBoard, or save it as a screenshot or a file?",
    },
    { id: 9, mondai: "How do I chat with other users on my DảkBoard" },
    { id: 10, mondai: "How do I create straight lines?" },
    { id: 11, mondai: "How can I donate for Draplus developer?" },
    {
        id: 12,
        mondai: "I have got a suggestion or I have found a bug. What should I do?",
    },
];

const kotaes = [
    {
        id: 1,
        mondai: "What can I do with Dảkboard?",
        kotae: "Draplus is a collaborating drawing software based on web application. System support user for many features such as: Collaborating drawing Quick take note Group chat View online people",
    },
    {
        id: 2,
        mondai: "How do I start using Dảkboard on Draplus Web?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 3,
        mondai: "How do I invite other users to my DăkBoard?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 4,
        mondai: "How do I interact with Dảkboard?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 5,
        mondai: "How do I change brush width and brush color?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 6,
        mondai: "Why can't I see the Clear All button?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 7,
        mondai: "How do I create a new board?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 8,
        mondai: "How can I export a DảkBoard, or save it as a screenshot or a file?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 9,
        mondai: "How do I chat with other users on my DảkBoard",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 10,
        mondai: "How do I create straight lines?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 11,
        mondai: "How can I donate for Draplus developer?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
    {
        id: 12,
        mondai: "I have got a suggestion or I have found a bug. What should I do?",
        kotae: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
    },
];

const UserGuide: React.FC<UserGuideProps> = () => {
    const [showDropDown, setShowDropDown] = React.useState(false);

    return (
        <div className="flex pt-28">
            {/* SideBar */}
            <div className="sideBar w-80">
                <div className="scrollContent grid grid-cols-1 gap-2 fixed ml-4 w-[18rem] max-w-[18rem]">
                    {mondais.map((mondai) => (
                        <a
                            href={`#${mondai.id}`}
                            key={mondai.id}
                            className="tabSelect self-center p-2"
                        >
                            {mondai.mondai}
                        </a>
                    ))}
                </div>
            </div>

            {/* Content */}
            <div className="scrollContent mx-4 w-[50rem]">
                {/* DropDown */}
                <div>
                    <div
                        className="dropDown hidden text-white p-2 mb-1"
                        onClick={() => setShowDropDown(!showDropDown)}
                    >
                        On this page
                    </div>

                    <div
                        className={
                            showDropDown
                                ? `frame z-10 list-none mb-2`
                                : `hidden`
                        }
                    >
                        <ul className="p-2">
                            {mondais.map((mondai) => (
                                <li key={mondai.id} className="text-white py-1">
                                    <a
                                        href={`#${mondai.id}`}
                                        onClick={() => setShowDropDown(false)}
                                    >
                                        {mondai.mondai}
                                    </a>
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>

                {/* Header */}
                <div>
                    <div className="text-5xl">Draplus DảkBoard Help</div>
                    <div className="mt-6">DảkBoard</div>
                </div>
                {/* Body */}
                <div className="mt-12">
                    {/* Introduce */}
                    <div>
                        Online group communication when discussing projects is
                        always a pain when sharing ideas among group members. We
                        design a solution for this problem by providing online
                        collaborating drawing and group chat included.
                    </div>
                    {/* Helps */}
                    {kotaes.map((kotae) => (
                        <div id={`${kotae.id}`} key={kotae.id} className="mt-8">
                            <div className="text-3xl">{kotae.mondai}</div>
                            <div>{kotae.kotae}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default UserGuide;
