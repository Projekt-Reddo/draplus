import * as React from "react";
import { Fragment } from "react";
import { Menu, Transition } from "@headlessui/react";
import "styles/Setting.css";
import { Link, useNavigate } from "react-router-dom";
import Icon from "components/Icon";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "store/actions";

interface AvatarProps {}

interface AvatarItem {
    icon: string;
    name: string;
    path: string;
    isClickable: boolean;
    onClick: () => void;
}

const Avatar: React.FC<AvatarProps> = () => {
    // Getting user from redux
    const user = useSelector((state: any) => state.user.user);

    // Getting current path from url
    const navigate = useNavigate();

    // Use distpatch to update redux
    const dispatch = useDispatch();

    // State manage Notification component
    const [toggle, setToggle] = React.useState(false);
    console.log(user);

    const AvatarItems: AvatarItem[] = [
        {
            icon: "",
            name: user.name,
            path: "",
            isClickable: false,
            onClick: () => {},
        },
        {
            icon: "",
            name: user.email,
            path: "",
            isClickable: false,
            onClick: () => {},
        },
        {
            icon: "fa-sign-out",
            name: "Logout",
            path: "",
            isClickable: true,
            onClick: () => {
                dispatch(logout());
                navigate("/");
            },
        },
    ];

    return (
        <>
            <Menu as="div">
                <div>
                    <Menu.Button className="rounded-full fixed origin-top-right right-24 top-7 setting-btn drop-shadow-md">
                        {/* <Icon icon="gear" fontSize="1.25rem" /> */}
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
                    <Menu.Items className="origin-top-right fixed right-10 top-20 mt-2 w-52 px-2 rounded-md shadow-lg setting-item divide-y divide-gray-400 ring-1 ring-black ring-opacity-5 focus:outline-none">
                        {AvatarItems.map((item: AvatarItem, index: number) => (
                            <div className="py-1" key={index}>
                                <Menu.Item>
                                    {({ active }) => (
                                        <div
                                            className={`setting-item cursor-pointer
                    ${active ? "text-slate-400" : "text-slate-100"}
                     block px-4 py-2`}
                                            onClick={item.onClick}
                                        >
                                            {item.icon !== "" ? (
                                                <Icon
                                                    className="mr-4"
                                                    icon={item.icon}
                                                    size="lg"
                                                />
                                            ) : (
                                                <></>
                                            )}
                                            {item.name}
                                        </div>
                                    )}
                                </Menu.Item>
                            </div>
                        ))}
                    </Menu.Items>
                </Transition>
            </Menu>

            {/* <Notification
                icon="circle-check"
                title="Copy to clipboard successfully"
                // message="Now you can share this link"
                toggle={toggle}
                setToggle={setToggle}
            /> */}
        </>
    );
};

export default Avatar;
