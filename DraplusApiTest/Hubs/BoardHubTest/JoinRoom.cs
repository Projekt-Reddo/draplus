using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
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
            var boardHub = new BoardHub(mockConnections.Object, mockBoardRepo.Object, mockMapper.Object, mockUserRepo.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };

            // Act
            await boardHub.JoinRoom(userConnection);

            // Assert
            VerifySomebodyAddedToGroup(Times.Once(), "board1");
        }
    }
}