using Microsoft.AspNetCore.SignalR;
using DraplusApi.Dtos;
using DraplusApi.Models;
using DraplusApi.Data;
using MongoDB.Driver;
using AutoMapper;
using static Constant;
using Newtonsoft.Json;

namespace DraplusApi.Hubs;

public class BoardHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;
    private readonly IDictionary<string, List<ShapeReadDto>> _shapeList;
    private readonly IDictionary<string, List<NoteDto>> _noteList;
    private readonly IUserRepo _userRepo;
    private readonly IBoardRepo _boardRepo;
    private readonly IMapper _mapper;

    public BoardHub(IDictionary<string, UserConnection> connections, IBoardRepo boardRepo, IMapper mapper, IUserRepo userRepo, IDictionary<string, List<ShapeReadDto>> shapeList, IDictionary<string, List<NoteDto>> noteList)
    {
        _connections = connections;
        _userRepo = userRepo;
        _boardRepo = boardRepo;
        _mapper = mapper;
        _shapeList = shapeList;
        _noteList = noteList;
    }

    #region Join & Leave room

    /// <summary>
    /// Allow user to join a room
    /// </summary>
    /// <param name="userConnection"></param>
    /// <returns></returns>
    public async Task JoinRoom(UserConnection userConnection)
    {
        // Add user to connection list & group
        _connections[Context.ConnectionId] = userConnection;

        // Load old notes data & create temp note list for that user
        await LoadNotesFromDb(userConnection);

        // Shape to store that user draw
        NewShapeList(userConnection.Board);

        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Board);
    }

    public async Task LoadInitShapes(string boardId)
    {
        var boardFromRepo = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", boardId));

        var shapesToReturn = _mapper.Map<ICollection<ShapeReadDto>>(boardFromRepo.Shapes);

        if (_shapeList.ContainsKey(boardId))
        {
            shapesToReturn = shapesToReturn.Concat(_shapeList[boardId]).ToList();
        }

        await Clients.Caller.SendAsync("InitShapes", shapesToReturn);
    }

    /// <summary>
    /// When user want to out that room
    /// </summary>
    /// <returns></returns>
    public async Task LeaveRoom()
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            // Set mouse move to False
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMouse, userConnection.User.Id, userConnection.User.Name, 0, 0, false);

            // Remove current user from Group & connections
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Board);
            _connections.Remove(Context.ConnectionId);

            // Save data when no one in room
            await SaveShapes(userConnection);
        }

        if (userConnection != null)
        {
            await OnlineUsers(userConnection.Board);
        }
    }

    /// <summary>
    /// Handle out room when user disconnect
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            // Set mouse move to False
            Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMouse, userConnection.User.Id, userConnection.User.Name, 0, 0, false);

            // Remove current user from Group & connections
            Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Board);
            _connections.Remove(Context.ConnectionId);

            // // Save data when no one in room
            SaveShapes(userConnection);
        }

        if (userConnection != null)
        {
            OnlineUsers(userConnection.Board);
        }

        return base.OnConnectedAsync();
    }

    protected async Task SaveShapes(UserConnection userConnection)
    {
        var remainingConnections = _connections.Values.Where(x => x.Board == userConnection.Board);

        if (remainingConnections.Count() == 0)
        {
            var boardFromRepo = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));

            foreach (var shape in _shapeList[userConnection.Board])
            {
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


                var shapeToUpdate = _mapper.Map<Shape>(shape);

                if (boardFromRepo.Shapes == null)
                {
                    boardFromRepo.Shapes = new List<Shape>();
                }

                boardFromRepo.Shapes.Add(shapeToUpdate);
            }

            var noteList = new List<Note>();

            foreach (var note in _noteList[userConnection.Board])
            {
                var noteToUpdate = _mapper.Map<Note>(note);

                noteList.Add(noteToUpdate);
            }

            boardFromRepo.Notes = noteList;

            var updateBoard = await _boardRepo.Update(userConnection.Board, boardFromRepo);

            _shapeList.Remove(userConnection.Board);
            _noteList.Remove(userConnection.Board);
        }
    }

    #endregion

    /// <summary>
    /// When user draw to board
    /// </summary>
    /// <param name="shape">What user draw (line, text, eraser)</param>
    /// <returns></returns>
    public async Task DrawShape(ShapeReadDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var temp = shape;

            var existShape = _shapeList[userConnection.Board].FirstOrDefault(s => s.Id == shape.Id);

            if (existShape is null)
            {
                _shapeList[userConnection.Board].Add(shape);
            }

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveShape, shape);
        }
    }

    /// <summary>
    /// Clear all data in board
    /// </summary>
    /// <returns></returns>
    public async Task ClearAll()
    {

        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var board = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));
            if (userConnection.User.Id.ToString() == board.UserId)
            {
                board.Shapes = new List<Shape>();
                await _boardRepo.Update(userConnection.Board, board);
                _shapeList[userConnection.Board].Clear();
                await Clients.Group(userConnection.Board).SendAsync(HubReturnMethod.ClearAll);
            }
        }
    }

    /// <summary>
    /// Current online users mouse position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="isMove"></param>
    /// <returns></returns>
    public async Task SendMouse(int x, int y, bool isMove)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveMouse, userConnection.User.Id, userConnection.User.Name, x, y, isMove);
        }
    }

    #region Current online user

    /// <summary>
    /// Number of online people
    /// </summary>
    /// <param name="boardId"></param>
    /// <returns></returns>
    public async Task SendOnlineUsers(string boardId)
    {
        await OnlineUsers(boardId);
    }

    protected Task OnlineUsers(string boardId)
    {
        var users = _connections.Values.Where(user => user.Board == boardId).Select(user => user.User);
        return Clients.Group(boardId).SendAsync(HubReturnMethod.OnlineUsers, users);
    }

    #endregion

    #region Note

    /// <summary>
    /// Load old notes
    /// </summary>
    /// <param name="boardId"></param>
    /// <returns></returns>
    public async Task LoadNotes(string boardId)
    {
        if (_noteList.TryGetValue(boardId, out List<NoteDto>? notes))
        {
            await Clients.Caller.SendAsync(HubReturnMethod.LoadNotes, _noteList[boardId]);
        }
    }

    /// <summary>
    /// When user create new note
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    public async Task NewNote(NoteDto note)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var existNote = _noteList[userConnection.Board].FirstOrDefault(s => s.Id == note.Id);

            if (existNote is null)
            {
                _noteList[userConnection.Board].Add(note);
            }

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveNewNote, note);
        }
    }

    /// <summary>
    /// When user change something in a note
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    public async Task UpdateNote(NoteUpdateDto note)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            _noteList[userConnection.Board].ForEach(n =>
            {
                if (n.Id == note.Id)
                {
                    n.Text = note.Text;
                }
            });

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveUpdateNote, note);
        }
    }

    /// <summary>
    /// When user delete a note
    /// </summary>
    /// <param name="noteId"></param>
    /// <returns></returns>
    public async Task DeleteNote(string noteId)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            _noteList[userConnection.Board] = _noteList[userConnection.Board].Where((s) => s.Id != noteId).ToList();

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveDeleteNote, noteId);
        }
    }

    #endregion

    #region Undo & Redo

    /// <summary>
    /// Undo method
    /// </summary>
    /// <param name="shapeId"></param>
    /// <returns></returns>
    public async Task Undo(string shapeId)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            _shapeList[userConnection.Board] = _shapeList[userConnection.Board].Where((s) => s.Id != shapeId).ToList();
            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveUndo, shapeId);
        }
    }

    /// <summary>
    /// Redo method
    /// </summary>
    /// <param name="shape"></param>
    /// <returns></returns>
    public async Task Redo(ShapeReadDto shape)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
        {
            var existShape = _shapeList[userConnection.Board].FirstOrDefault(s => s.Id == shape.Id);

            if (existShape is null)
            {
                _shapeList[userConnection.Board].Add(shape);
            }

            await Clients.OthersInGroup(userConnection.Board).SendAsync(HubReturnMethod.ReceiveShape, shape);
        }
    }

    #endregion

    #region Handle Dictionary

    public void NewShapeList(string boardId)
    {
        if (!_shapeList.ContainsKey(boardId))
        {
            _shapeList[boardId] = new List<ShapeReadDto>();
        }
    }

    public void NewNoteList(string noteId)
    {
        if (!_noteList.ContainsKey(noteId))
        {
            _noteList[noteId] = new List<NoteDto>();
        }
    }

    #endregion

    #region Database CRUD

    protected async Task LoadNotesFromDb(UserConnection userConnection)
    {
        var connectionsOnABoard = _connections.Values.Where(x => x.Board == userConnection.Board);

        if (connectionsOnABoard.Count() == 0)
        {
            var boardFromRepo = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", userConnection.Board));

            if (boardFromRepo != null)
            {
                var notesList = boardFromRepo.Notes;

                if (notesList != null)
                {
                    _noteList[userConnection.Board] = _mapper.Map<List<NoteDto>>(notesList);
                }
            }
        }

        NewNoteList(userConnection.Board);
    }

    #endregion
}
