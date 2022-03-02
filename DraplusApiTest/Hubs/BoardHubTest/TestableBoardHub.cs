using System.Collections.Generic;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using Moq;
using NUnit.Framework;
using SignalR_UnitTestingSupport.Hubs;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    [TestFixture]
    public class TestableBoardHub : HubUnitTestsBase
    {
        public Mock<IMapper> mockMapper { get; set; } = null!;
        public Mock<IUserRepo> mockUserRepo { get; set; } = null!;
        public Mock<IBoardRepo> mockBoardRepo { get; set; } = null!;
        public Mock<IDictionary<string, UserConnection>> mockConnections { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            // Mock hub dependencies
            mockMapper = new Mock<IMapper>();
            mockUserRepo = new Mock<IUserRepo>();
            mockBoardRepo = new Mock<IBoardRepo>();
            mockConnections = new Mock<IDictionary<string, UserConnection>>();
        }
    }
}