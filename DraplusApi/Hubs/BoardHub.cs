using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;
using DraplusApi.Data;
using MongoDB.Driver;
using AutoMapper;
using Newtonsoft.Json;

namespace DraplusApi.Hubs;


public class BoardHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;
    private readonly IBoardRepo _boardRepo;
    private readonly IMapper _mapper;

    public BoardHub(IDictionary<string, UserConnection> connections, IBoardRepo boardRepo, IMapper mapper)
    {
        _connections = connections;
        _boardRepo = boardRepo;
        _mapper = mapper;
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;

    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            _connections.Remove(Context.ConnectionId);
        }

        return base.OnConnectedAsync();
    }

    public async Task DrawShape(ShapeCreateDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync("ReceiveShape", shape);

            // var jsonData = Convert.ToString(shape.Data);

            // try
            // {
            //     var data = JsonConvert.DeserializeObject<LinePathData>(jsonData);
            //     shape.Data = data;
            // }
            // catch (Exception e)
            // {
            //     var data = JsonConvert.DeserializeObject<TextData>(jsonData);
            //     shape.Data = data;
            // }

            // var boardFromRepo = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));
            // var shapeToUpdate = _mapper.Map<Shape>(shape);

            // if (boardFromRepo.Shapes == null)
            // {
            //     boardFromRepo.Shapes = new List<Shape>();
            // }

            // boardFromRepo.Shapes.Add(shapeToUpdate);
            // var updateBoard = await _boardRepo.Update(userConnection.Board, boardFromRepo);
        }
    }
}
