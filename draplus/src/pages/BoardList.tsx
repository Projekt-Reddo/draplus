import * as React from "react";
import BoardCard from "components/BoardCard";
import Setting from "components/Setting";
import Icon from "components/Icon";
import { useQuery } from "react-query";
import { API } from "utils/constant";
import Loading from "components/Loading";
import { useSelector } from "react-redux";

interface BoardListProps {}

interface Board {
    id: string;
    name?: string;
    createdAt?: string;
    lastEdit?: string;
    img?: string;
    chatRoomId?: string;
}

const BoardList: React.FC<BoardListProps> = () => {
    const userFromStore = useSelector((state: any) => state.user);

    console.log(userFromStore);

    const { isLoading, isError, data, error } = useQuery("boards", async () => {
        var rs = await fetch(`${API}/api/board/${userFromStore.user.id}`);
        return rs.json();
    });

    return (
        <div className="max-w-full min-h-screen bg-[color:var(--bg)]">
            {isLoading ? (
                <div className="w-full h-screen flex justify-center items-center">
                    <Loading />
                </div>
            ) : isError ? (
                <div className="w-full h-screen flex justify-center items-center">
                    <div>error</div>
                </div>
            ) : (
                <div className="grid grid-flow-row sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 gap-8 pt-24 pb-8 px-20">
                    {/* Create new board */}
                    <CreateBoard />

                    {/* User boards */}
                    {data.map((board: Board) => (
                        <BoardCard
                            key={board.id}
                            id={board.id}
                            name={board.name}
                            createdAt={board.createdAt}
                            lastEdit={board.lastEdit}
                            img={board.img}
                        />
                    ))}
                </div>
            )}

            <Setting />
        </div>
    );
};

export default BoardList;

interface CreateBoardProps {
    onClick?: () => void;
}

const CreateBoard: React.FC<CreateBoardProps> = ({ onClick }) => {
    return (
        <div
            className="max-w-sm h-96 shadow-lg border border-gray-300 rounded-2xl flex justify-center items-center"
            onClick={onClick ? onClick : () => {}}
        >
            <div className="rounded-full h-[3rem] w-[3rem] bg-[color:var(--element-bg)] flex justify-center items-center">
                <Icon icon="plus" className=" text-white bold" size="xl" />
            </div>
        </div>
    );
};
