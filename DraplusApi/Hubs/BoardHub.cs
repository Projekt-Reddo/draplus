using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Hubs;


public class BoardHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;

    public BoardHub(IDictionary<string, UserConnection> connections)
    {
        _connections = connections;
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;

    }

    public async Task DrawShape(User user, ShapeCreateDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync("ReceiveShape", user, shape);
        }
    }
}
