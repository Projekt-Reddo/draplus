using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;
using static Constant;

namespace DraplusApi.Hubs;

public class ChatHub : Hub
{
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnectionChat> _connections;

    public ChatHub(IDictionary<string, UserConnectionChat> connections)
    {
        _botUser = "What I'm I doing here?";
        _connections = connections;
    }

    public async Task JoinRoom(UserConnectionChat userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;
    }

    public async Task LeaveRoom()
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnectionChat? userConnection))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Board);

            _connections.Remove(Context.ConnectionId);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnectionChat? userConnection))
        {
            _connections.Remove(Context.ConnectionId);
        }

        return base.OnConnectedAsync();
    }

    public async Task SendMessage(User user, string message)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnectionChat? userConnection))
        {
            await Clients.Group(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMessage, user, message, DateTime.Now);

        }
    }
}
