using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using DraplusApi.Models;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using static Constant;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    public class ClearAll : TestableBoardHub
    {
        [Test]
        public async Task ClearAll_Succces()
        {
            // Arrange
            // Create a boardHub for simulate
            var boardHub = new BoardHub(
                connections: mockConnections.Object,
                boardRepo: mockBoardRepo.Object,
                mapper: mockMapper.Object,
                userRepo: mockUserRepo.Object,
                shapeList: mockShapeList.Object,
                noteList: mockNoteList.Object);
            AssignToHubRequiredProperties(boardHub);

            UserConnection userConnection = new UserConnection { Board = "board1", User = new UserInfoInUserConnection() { Id = "1" } };
            mockConnections.Setup(connections =>
            connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true);

            mockBoardRepo.Setup(board =>
            board.GetByCondition(It.IsAny<FilterDefinition<Board>>())).ReturnsAsync(new Board() { UserId = userConnection.User.Id });

            mockShapeList.Setup(list =>
            list[It.IsAny<string>()]
            ).Returns(new List<ShapeReadDto>());
            // Act
            await boardHub.ClearAll();

            // Assert
            ClientsGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ClearAll, new object[] { },
                    It.IsAny<CancellationToken>()), Times.Once);

        }

    }
}