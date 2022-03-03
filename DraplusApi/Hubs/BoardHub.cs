using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;
using DraplusApi.Data;
using MongoDB.Driver;
using AutoMapper;

using static Constant;

namespace DraplusApi.Hubs;

public class BoardHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;
    private readonly IDictionary<string, List<ShapeReadDto>> _shapeList;
    private readonly IUserRepo _userRepo;
    private readonly IBoardRepo _boardRepo;
    private readonly IMapper _mapper;
    
    public BoardHub(IDictionary<string, UserConnection> connections, IBoardRepo boardRepo, IMapper mapper, IUserRepo userRepo, IDictionary<string, List<ShapeReadDto>> shapeList)
    {
        _connections = connections;
        _userRepo = userRepo;
        _boardRepo = boardRepo;
        _mapper = mapper;
        _shapeList = shapeList;
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);

        _connections[Context.ConnectionId] = userConnection;

        if (!_shapeList.ContainsKey(userConnection.Board))
        {
            _shapeList[userConnection.Board] = new List<ShapeReadDto>();
        }

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
            OnlineUsers(userConnection.Board);
        }
        return base.OnConnectedAsync();
    }

    public async Task DrawShape(ShapeReadDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var temp = shape;

            _shapeList[userConnection.Board].Add(shape);

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveShape, shape);

            // var jsonData = Convert.ToString(shape.Data);

            // try
            // {
            //     var data = JsonConvert.DeserializeObject<LinePathData>(jsonData);
            //     shape.Data = data;
            // }
            // catch
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
    public async Task ClearAll()
    {
        
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var temp = userConnection.Board;
            var board = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));
            if (userConnection.User.ToString() != board.UserId)
            {
                await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ClearAll, 0);
            }
            board.Shapes = new List<Shape>();
            await _boardRepo.Update(temp, board);
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ClearAll,1);
        }
    }

    public async Task SendMouse(int x, int y, bool isMove)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMouse, userConnection.User.Id, userConnection.User.Name, x, y, isMove);
        }
    }

    public async Task SendOnlineUsers(string boardId)
    {
        await OnlineUsers(boardId);
    }

    public Task OnlineUsers(string boardId)
    {
        var users = _connections.Values.Where(user => user.Board == boardId).Select(user => user.User);
        return Clients.Group(boardId).SendAsync(HubReturnMethod.OnlineUsers, users);
    }

    public async Task NewNote(NoteDto note)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveNewNote, note);
        }
    }

    public async Task UpdateNote(NoteUpdateDto note)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveUpdateNote, note);
        }
    }

    public async Task DeleteNote(string noteId)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveDeleteNote, noteId);
        }
    }

    public async Task Undo(string shapeId)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveUndo, shapeId);
        }
    }

    public async Task Redo(ShapeReadDto shape) {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveShape, shape);
        }
    }
}
