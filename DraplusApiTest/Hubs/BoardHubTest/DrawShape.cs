using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using Moq;
using NUnit.Framework;
using static Constant;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    public class DrawShape : TestableBoardHub
    {
        [Test]
        public async Task DrawShape_LinePathData_Success()
        {
            // Arrange
            var boardHub = new BoardHub(mockConnections.Object, mockBoardRepo.Object, mockMapper.Object, mockUserRepo.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };
            mockConnections.Setup(connections => connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true); // ! after userConnection is for null forgiving

            var shape = new ShapeCreateDto { Data = new List<string> { "1", "2", "3", "4" } };

            // Act
            await boardHub.DrawShape(shape);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveShape, new object[] { shape }, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}