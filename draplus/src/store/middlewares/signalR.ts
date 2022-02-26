import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import {
    JOIN_ROOM,
    LEAVE_ROOM,
    RECEIVE_MESSAGE,
    SEND_MESSAGE,
    RECEIVE_SHAPE,
    DRAW_SHAPE,
    SEND_MOUSE,
    RECEIVE_MOUSE,
    ONLINE_USERS,
    GET_ONLINE_USERS,
} from "store/actions";
import { API } from "utils/constant";

var connection: {
    [key: string]: HubConnection;
} = {};

export const signalRMiddleware = (storeAPI: any) => {
    return (next: any) => async (action: any) => {
        if (action.type === JOIN_ROOM) {
            connection.board = await createSignalRConnection(`${API}/board`);
            connection.chat = await createSignalRConnection(`${API}/chat`);

            await connection.board.invoke("JoinRoom", {
                user: action.payload.user,
                board: action.payload.board,
            });

            // await connection.chat.invoke("JoinRoom", {
            //     user: action.payload.user,
            //     board: action.payload.board,
            // });

            connection.chat.on(
                "ReceiveMessage",
                (user: any, message: string, timestamp: Date) => {
                    storeAPI.dispatch({
                        type: RECEIVE_MESSAGE,
                        payload: {
                            user,
                            message,
                            timestamp,
                        },
                    });
                }
            );

            connection.board.on("ReceiveShape", (shape: any) => {
                storeAPI.dispatch({
                    type: RECEIVE_SHAPE,
                    payload: shape,
                });
            });

            connection.board.on(
                "ReceiveMouse",
                (
                    userId: string,
                    userName: string,
                    x: number,
                    y: number,
                    isMove: boolean
                ) => {
                    storeAPI.dispatch({
                        type: RECEIVE_MOUSE,
                        payload: {
                            userId,
                            userName,
                            x,
                            y,
                            isMove,
                        },
                    });
                }
            );

            connection.board.on("OnlineUsers", (users: object[]) => {
                storeAPI.dispatch({
                    type: ONLINE_USERS,
                    payload: users,
                });
            });

            connection.chat.onclose(() => {});
            connection.board.onclose(() => {});
        }

        if (action.type === LEAVE_ROOM) {
            try {
                connection.board.stop();
                connection.chat.stop();
            } catch (e) {
                console.log(e);
            }
        }

        if (action.type === SEND_MESSAGE) {
            connection.chat.invoke(
                "SendMessage",
                action.payload.user,
                action.payload.message
            );
        }

        if (action.type === DRAW_SHAPE) {
            connection.board.invoke("DrawShape", action.payload);
        }

        if (action.type === SEND_MOUSE && connection.board) {
            connection.board.invoke(
                "SendMouse",
                action.payload.x,
                action.payload.y,
                action.payload.isMove
            );
        }

        if (action.type === GET_ONLINE_USERS && connection.board) {
            connection.board.invoke("SendOnlineUsers", action.payload);
        }

        return next(action);
    };
};

async function createSignalRConnection(url: string) {
    const newConnection = new HubConnectionBuilder()
        .withUrl(url)
        .withAutomaticReconnect()
        .build();

    await newConnection.start();

    return newConnection;
}
