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
    public class NewNote : TestableBoardHub
    {
        [Test]
        public async Task NewNote_Success()
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

            mockNoteList.Setup(list => list[It.IsAny<string>()]).Returns(new List<NoteDto>()); // ! after note is for null forgiving

            var note = new NoteDto { Id = "1", X = 99, Y = 99, Text = "New Art" };

            // Act
            await boardHub.NewNote(note);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveNewNote, new object[] { note }, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}