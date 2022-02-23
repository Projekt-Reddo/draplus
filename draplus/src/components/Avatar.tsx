import * as React from "react";
import { Fragment } from "react";
import { Menu, Transition } from "@headlessui/react";
import "styles/Setting.css";
import { useNavigate } from "react-router-dom";
import Icon from "components/Icon";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "store/actions";

interface AvatarProps {}

interface AvatarItem {
    icon: string;
    name: string;
    className: string;
    style: object;
    isClickable: boolean;
    onClick: () => void;
}

interface AvatarItems {
    username: AvatarItem;
    email: AvatarItem;
    logout: AvatarItem;
}

const Avatar: React.FC<AvatarProps> = () => {
    // Getting user from redux
    const user = useSelector((state: any) => state.user.user);

    // Getting current path from url
    const navigate = useNavigate();

    // Use distpatch to update redux
    const dispatch = useDispatch();

    const AvatarItems: AvatarItems = {
        username: {
            icon: "",
            name: user.name,
            isClickable: false,
            style: {},
            className: "",
            onClick: () => {},
        },
        email: {
            icon: "",
            name: user.email,
            style: {},
            className: "text-xs",
            isClickable: false,
            onClick: () => {},
        },
        logout: {
            icon: "fa-sign-out",
            name: "Logout",
            style: { color: "#33CDFF" },
            className: "",
            isClickable: true,
            onClick: () => {
                dispatch(logout());
                navigate("/");
            },
        },
    };
    console.log(user);

    return (
        <>
            <Menu as="div">
                <div>
                    <Menu.Button className="rounded-full fixed origin-top-right right-24 top-7 setting-btn drop-shadow-md">
                        <div className="avatar">
                            <img
                                className="inline-block rounded-full ring-2 ring-white"
                                src={user.avatar}
                                alt="user avatar"
                            />
                        </div>
                    </Menu.Button>
                </div>

                <Transition
                    as={Fragment}
                    enter="transition ease-out duration-100"
                    enterFrom="transform opacity-0 scale-95"
                    enterTo="transform opacity-100 scale-100"
                    leave="transition ease-in duration-75"
                    leaveFrom="transform opacity-100 scale-100"
                    leaveTo="transform opacity-0 scale-95"
                >
                    <Menu.Items
                        className="origin-top-right fixed right-10 top-20 mt-2 w-52 px-2 rounded-md shadow-lg 
                    setting-item divide-y divide-gray-400 ring-1 ring-black ring-opacity-5 focus:outline-none
                    "
                    >
                        <div className="py-1">
                            <Menu.Item>
                                {({ active }) => (
                                    <div
                                        className={`cursor-pointer overflow-hidden whitespace-nowrap text-ellipsis
                    ${active ? "text-slate-400" : "text-slate-100"}
                    px-2 pt-1 text-right 
                    `}
                                    >
                                        <Icon
                                            className="mr-4"
                                            icon={AvatarItems.username.icon}
                                            color="#33CDFF"
                                            size="lg"
                                        />
                                        <p
                                            className={`w-100 inline ${AvatarItems.username.className}"}`}
                                            style={AvatarItems.username.style}
                                        >
                                            {AvatarItems.username.name}
                                        </p>
                                    </div>
                                )}
                            </Menu.Item>
                            <Menu.Item>
                                {({ active }) => (
                                    <div
                                        className={`cursor-pointer overflow-hidden whitespace-nowrap text-ellipsis
                    ${active ? "text-slate-400" : "text-slate-100"}
                    px-2 pb-1 text-right 
                    `}
                                    >
                                        <Icon
                                            className="mr-4"
                                            icon={AvatarItems.email.icon}
                                            color="#33CDFF"
                                            size="lg"
                                        />
                                        <p
                                            className={`w-100 inline ${AvatarItems.email.className} "}`}
                                            style={AvatarItems.email.style}
                                        >
                                            {AvatarItems.email.name}
                                        </p>
                                    </div>
                                )}
                            </Menu.Item>
                        </div>
                        <div className="py-1">
                            <Menu.Item>
                                {({ active }) => (
                                    <div
                                        className={`cursor-pointer overflow-hidden whitespace-nowrap text-ellipsis
                    ${active ? "text-slate-400" : "text-slate-100"}
                    px-2 py-2 text-right 
                    `}
                                        onClick={AvatarItems.logout.onClick}
                                    >
                                        <Icon
                                            className="mr-4"
                                            icon={AvatarItems.logout.icon}
                                            color="#33CDFF"
                                            size="lg"
                                        />
                                        <p
                                            className={`w-100 inline 
                                                ${AvatarItems.logout.className}"}`}
                                            style={AvatarItems.logout.style}
                                        >
                                            {AvatarItems.logout.name}
                                        </p>
                                    </div>
                                )}
                            </Menu.Item>
                        </div>
                    </Menu.Items>
                </Transition>
            </Menu>
        </>
    );
};

export default Avatar;
