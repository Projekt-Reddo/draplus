using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Hubs;

public class ChatHub : Hub {
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connections;

    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "What I'm I doing here?";
        _connections = connections;
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;
    }

    public async Task SendMessage(User user, string message)
    {
        // if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection)) {
        //     await Clients.Group(userConnection.Board).SendAsync("ReceiveMessage", user, message);

        // }

        await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now);
    }
}
