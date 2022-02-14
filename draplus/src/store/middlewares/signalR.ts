import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import {
    LOGIN,
    RECEIVE_MESSAGE,
    SEND_MESSAGE,
    RECEIVE_SHAPE,
    DRAW_SHAPE,
} from "store/actions";
import { API } from "utils/constant";

var connection: {
    [key: string]: HubConnection;
} = {};

export const signalRMiddleware = (storeAPI: any) => {
    return (next: any) => async (action: any) => {
        if (action.type === LOGIN) {
            connection.board = await createSignalRConnection(`${API}/board`);
            connection.chat = await createSignalRConnection(`${API}/chat`);

            await connection.board.invoke("JoinRoom", {
                board: "62099e84045bcbc6c47bc749",
            });

            connection.chat.on(
                "ReceiveMessage",
                (user: string, message: string) => {
                    storeAPI.dispatch({
                        type: RECEIVE_MESSAGE,
                        payload: {
                            user,
                            message,
                        },
                    });
                }
            );

            connection.board.on("ReceiveShapes", (user: string, shape: any) => {
                storeAPI.dispatch({
                    type: RECEIVE_SHAPE,
                    payload: {
                        user,
                        shape,
                    },
                });
            });

            connection.chat.onclose(() => {});
            connection.board.onclose(() => {});
        }

        if (action.type === SEND_MESSAGE) {
            connection.chat.invoke("SendMessage", action.payload);
        }

        if (action.type === DRAW_SHAPE) {
            connection.board.invoke("DrawShape", action.payload);
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
