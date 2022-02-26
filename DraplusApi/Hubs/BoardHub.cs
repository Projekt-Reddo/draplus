using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;
using DraplusApi.Data;
using MongoDB.Driver;
using AutoMapper;
using Newtonsoft.Json;
using static Constant;

namespace DraplusApi.Hubs;


public class BoardHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;
    private readonly IUserRepo _userRepo;
    private readonly IBoardRepo _boardRepo;
    private readonly IMapper _mapper;

    public BoardHub(IDictionary<string, UserConnection> connections, IBoardRepo boardRepo, IMapper mapper, IUserRepo userRepo)
    {
        _connections = connections;
        _userRepo = userRepo;
        _boardRepo = boardRepo;
        _mapper = mapper;
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;

        await SendOnlineUsers(userConnection.Board);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            _connections.Remove(Context.ConnectionId);

            Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Board);
        }

        if (userConnection != null)
        {
            SendOnlineUsers(userConnection.Board);
        }
        return base.OnConnectedAsync();
    }

    public async Task DrawShape(ShapeCreateDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveShape, shape);

            var jsonData = Convert.ToString(shape.Data);

            try
            {
                var data = JsonConvert.DeserializeObject<LinePathData>(jsonData);
                shape.Data = data;
            }
            catch
            {
                var data = JsonConvert.DeserializeObject<TextData>(jsonData);
                shape.Data = data;
            }

            var boardFromRepo = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));
            var shapeToUpdate = _mapper.Map<Shape>(shape);

            if (boardFromRepo.Shapes == null)
            {
                boardFromRepo.Shapes = new List<Shape>();
            }

            boardFromRepo.Shapes.Add(shapeToUpdate);
            var updateBoard = await _boardRepo.Update(userConnection.Board, boardFromRepo);
        }
    }

    public async Task SendMouse(int x, int y, bool isMove)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMouse, userConnection.User.Id, userConnection.User.Name, x, y, isMove);
        }
    }

    public Task SendOnlineUsers(string boardId)
    {
        var users = _connections.Values.Where(user => user.Board == boardId).Select(user => user.User);

        return Clients.Group(boardId).SendAsync(HubReturnMethod.OnlineUsers, users);
    }
}
