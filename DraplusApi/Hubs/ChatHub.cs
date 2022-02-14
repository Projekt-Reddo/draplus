using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;

namespace DraplusApi.Hubs;

public class ChatHub : Hub {
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connections;

    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "What I'm I doing here?";
        _connections = connections;
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
