import * as React from "react";
import Icon from "components/Icon";
import moment from "moment";
import { DefaultDay } from "utils/constant";
import { Menu, Transition } from "@headlessui/react";
import Notification from "./Notification";
import { useNavigate } from "react-router-dom";

interface BoardCardProps {
    id: string;
    name?: string;
    createdAt?: string;
    lastEdit?: string;
    img?: string;
}

const BoardCard: React.FC<BoardCardProps> = ({
    id,
    name,
    createdAt,
    lastEdit,
    img,
}) => {
    const nagivate = useNavigate();

    const goToBoard = () => {
        nagivate(`/board/${id}`);
    };

    return (
        <>
            <div className="max-w-sm h-96 relative overflow-hidden shadow-lg border border-gray-300 rounded-2xl">
                {img ? (
                    <img
                        className="w-full h-[73%] object-cover cursor-pointer"
                        src={img}
                        alt="Board"
                        onClick={goToBoard}
                    />
                ) : (
                    <div
                        className="w-full h-[73%] bg-[color:var(--bg)] cursor-pointer"
                        onClick={goToBoard}
                    />
                )}

                <div className="w-full h-[27%] px-6 py-4 bg-white flex flex- align-middle">
                    <div className="flex flex-col justify-center w-4/5">
                        <div className="font-bold text-xl mb-2 overflow-hidden">
                            {name ? name : "Last edited"}
                        </div>
                        <p className="text-gray-500 text-base">
                            <Icon icon="clock" className="mr-2" />
                            {moment(
                                lastEdit === DefaultDay ? createdAt : lastEdit
                            ).format("DD MMMM YYYY")}
                        </p>
                    </div>
                    <div className="w-1/5">
                        <BoardCardOptions />
                    </div>
                </div>
            </div>
        </>
    );
};

export default BoardCard;

interface BoardCardOptionsProps {}

interface Option {
    icon: string;
    label: string;
    onClick?: () => void;
}

const BoardCardOptions: React.FC<BoardCardOptionsProps> = () => {
    const Options: Option[] = [
        {
            icon: "trash",
            label: "Delete",
            onClick: () => {
                setToggle(true);
            },
        },
        {
            icon: "share-square",
            label: "Export",
        },
    ];

    // State manage Notification component
    const [toggle, setToggle] = React.useState(false);

    return (
        <>
            <Menu as="div">
                <div>
                    <Menu.Button className="rounded-full absolute origin-bottom-right right-8 bottom-9 z-40 drop-shadow-md text-gray-700">
                        <Icon icon="ellipsis-vertical" fontSize="1.25rem" />
                    </Menu.Button>
                </div>

                <Transition
                    as={React.Fragment}
                    enter="transition ease-out duration-100"
                    enterFrom="transform opacity-0 scale-95"
                    enterTo="transform opacity-100 scale-100"
                    leave="transition ease-in duration-75"
                    leaveFrom="transform opacity-100 scale-100"
                    leaveTo="transform opacity-0 scale-95"
                >
                    <Menu.Items className="origin-bottom-right absolute right-2 bottom-20 mt-2 w-48 px-2 rounded-2xl shadow-lg bg-white divide-y divide-gray-400 ring-1 ring-black ring-opacity-5 focus:outline-none">
                        <div className="py-1">
                            {Options.map((option, index) => (
                                <Menu.Item key={index}>
                                    {({ active }) => (
                                        <div
                                            className={`text-xl cursor-pointer
                  ${active ? "text-gray-900" : "text-gray-600"}
                   block px-4 py-3`}
                                            onClick={
                                                option.onClick
                                                    ? option.onClick
                                                    : () => {}
                                            }
                                        >
                                            <Icon
                                                className="mr-3"
                                                icon={option.icon}
                                                size="lg"
                                            />
                                            {option.label}
                                        </div>
                                    )}
                                </Menu.Item>
                            ))}
                        </div>
                    </Menu.Items>
                </Transition>
            </Menu>

            <Notification
                icon="circle-check"
                title="Delete board successfully"
                toggle={toggle}
                setToggle={setToggle}
            />
        </>
    );
};
