using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using DraplusApi.Models;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    public class JoinRoom : TestableBoardHub
    {
        [Test]
        public async Task JoinRoom_Board_Success()
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

            UserConnection userConnection = new UserConnection
            {
                Board = "board1",
                User = new UserInfoInUserConnection
                {
                    Id = "1234"
                }
            };

            mockBoardRepo.Setup(board => board.GetByCondition(It.IsAny<FilterDefinition<Board>>())).ReturnsAsync(new Board { Id = "board1" });

            mockConnections.SetupGet(x => x.Values).Returns(new List<UserConnection>(){
                userConnection
            });

            // Act
            await boardHub.JoinRoom(userConnection);

            // Assert
            VerifySomebodyAddedToGroup(Times.Once(), "board1");
        }
    }
}