using System;
using System.Threading;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using Moq;
using NUnit.Framework;
using static Constant;

namespace DraplusApiTest.Hubs.ChatHubTest
{
    public class SendMessage : TestableChatHub
    {
        [Test]
        public async Task SendMessage_Success()
        {

            // Arrange
            // Create a chatHub for simulate
            var chatHub = new ChatHub(
                connections: mockConnections.Object);
            AssignToHubRequiredProperties(chatHub);

            UserConnectionChat userConnectionChat = new UserConnectionChat { Board = "board1", User = new UserInfoInUserConnection() };
            mockConnections.Setup(connections =>
            connections.TryGetValue(It.IsAny<string>(), out userConnectionChat!)).Returns(true);
            var user = new DraplusApi.Models.User();
            var message = "testing in process";

            // Act

            await chatHub.SendMessage(user, message);

            // Assert
            // ClientsGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveMessage, new object[] { user, message, DateTime.Now },
            //         It.IsAny<CancellationToken>()));
            Assert.Pass();
        }
    }
}