using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using Moq;
using NUnit.Framework;
using static Constant;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    public class UpdateNote : TestableBoardHub
    {
        [Test]
        public async Task UpdateNote_Success()
        {
            // Arrange
            var boardHub = new BoardHub(
                connections: mockConnections.Object,
                boardRepo: mockBoardRepo.Object,
                mapper: mockMapper.Object,
                userRepo: mockUserRepo.Object,
                shapeList: mockShapeList.Object,
                noteList: mockNoteList.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };
            mockConnections.Setup(connections => connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true); // ! after userConnection is for null forgiving

            mockNoteList.Setup(list => list[It.IsAny<string>()]).Returns(new List<NoteDto>() {
                new NoteDto() {Id = "1", X = 83, Y = 382, Text = "A"},
                new NoteDto() {Id = "2", X = 21, Y = 248, Text = "A"},
                new NoteDto() {Id = "3", X = 432, Y = 544, Text = "A"},
                new NoteDto() {Id = "4", X = 602, Y = 691, Text = "A"},
            }); // ! after note is for null forgiving

            var note = new NoteUpdateDto { Id = "2", Text = "Draplus Draw" };

            // Act
            await boardHub.UpdateNote(note);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveUpdateNote, new object[] { note }, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdateNote_NoteIdNotExist_Fail()
        {
            // Arrange
            var boardHub = new BoardHub(
                connections: mockConnections.Object,
                boardRepo: mockBoardRepo.Object,
                mapper: mockMapper.Object,
                userRepo: mockUserRepo.Object,
                shapeList: mockShapeList.Object,
                noteList: mockNoteList.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };
            mockConnections.Setup(connections => connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true); // ! after userConnection is for null forgiving

            mockNoteList.Setup(list => list[It.IsAny<string>()]).Returns(new List<NoteDto>() {
                new NoteDto() {Id = "1", X = 83, Y = 382, Text = "A"},
                new NoteDto() {Id = "2", X = 21, Y = 248, Text = "A"},
                new NoteDto() {Id = "3", X = 432, Y = 544, Text = "A"},
                new NoteDto() {Id = "4", X = 602, Y = 691, Text = "A"},
            }); // ! after note is for null forgiving

            var note = new NoteUpdateDto { Id = "9", Text = "Draplus Draw" };

            // Act
            await boardHub.UpdateNote(note);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveUpdateNote, new object[] { note }, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}