using System.Collections.Generic;
using DraplusApi.Dtos;
using Moq;
using NUnit.Framework;
using SignalR_UnitTestingSupport.Hubs;

namespace DraplusApiTest.Hubs.ChatHubTest
{
    public class TestableChatHub : HubUnitTestsBase
    {
        public Mock<IDictionary<string, UserConnectionChat>> mockConnections { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            // Mock hub dependencies
            mockConnections = new Mock<IDictionary<string, UserConnectionChat>>();
        }
    }
}