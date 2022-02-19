import * as React from "react";
import BoardCard from "components/BoardCard";
import Setting from "components/Setting";

interface BoardListProps {}

interface Board {
    id: number;
    title?: string;
    createdAt?: string;
    updatedAt?: string;
    img?: string;
}

const Boards: Board[] = [
    {
        id: 1,
        title: "Mountain",
        createdAt: "2020-01-01",
        updatedAt: "2020-01-01",
        img: "https://i.ibb.co/fpN5pbB/97c5c6c625ecd2b28bfd.jpg",
    },
    {
        id: 2,
        title: "Test",
        createdAt: "2020-01-01",
        updatedAt: "2020-01-01",
        img: "https://i.ibb.co/Cn0G3W7/60084041-p0-master1200.jpg",
    },
    {
        id: 3,
        title: "Your board",
        createdAt: "2020-01-01",
        updatedAt: "2020-01-01",
        img: "https://i.ibb.co/J5jrcnp/Untitled-000000.png",
    },
    {
        id: 4,
        title: "Mountain",
        createdAt: "0001-01-01T00:00:00.000+00:00",
        updatedAt: "2020-01-01",
        img: "https://i.ibb.co/t2368Xb/20210514-102151.jpg",
    },
    {
        id: 5,
        title: "Mountain",
        createdAt: "2022-02-15T00:18:08.319+00:00",
        updatedAt: "0001-01-01T00:00:00.000+00:00",
        // img: "https://i.ibb.co/p2kmwvL/N11-2-1920-x-1080.jpg",
    },
    {
        id: 6,
        // title: "Mountain",
        createdAt: "2022-02-16T00:18:08.319+00:00",
        updatedAt: "0001-01-01T00:00:00.000+00:00",
        img: "https://i.ibb.co/NKPR6vt/93832168-p0.png",
    },
];

const BoardList: React.FC<BoardListProps> = () => {
    return (
        <div className="max-w-full min-h-screen bg-[color:var(--bg)]">
            <div className="grid grid-flow-row sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 gap-8 pt-24 pb-8 px-20">
                {Boards.map((board) => (
                    <BoardCard
                        key={board.id}
                        id={board.id}
                        title={board.title}
                        createdAt={board.createdAt}
                        updatedAt={board.updatedAt}
                        img={board.img}
                    />
                ))}
            </div>
            <Setting />
        </div>
    );
};

export default BoardList;
