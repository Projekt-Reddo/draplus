import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { LOGIN, RECEIVE_MESSAGE, SEND_MESSAGE } from "store/actions";
import { API } from "utils/constant";

var connection: {
    [key: string]: HubConnection;
} = {};

export const signalRMiddleware = (storeAPI: any) => {
    return (next: any) => async (action: any) => {
        if (action.type === LOGIN) {
            connection.chat = await createSignalRConnection(`${API}/chat`);

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

            connection.chat.onclose(() => {});
        }

        if (action.type === SEND_MESSAGE) {
            connection.chat.invoke("SendMessage", action.payload);
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
