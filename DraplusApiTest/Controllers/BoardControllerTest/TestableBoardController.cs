using AutoMapper;
using DraplusApi.Data;
using Moq;
using NUnit.Framework;

namespace DraplusApiTest.Controllers.BoardControllerTest
{
    public class TestableBoardController
    {
        public Mock<IBoardRepo> mockBoardRepo { get; set; } = null!;
        public Mock<IUserRepo> mockUserRepo { get; set; } = null!;
        public Mock<IMapper> mockMapper { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            // Mock controller dependencies
            mockBoardRepo = new Mock<IBoardRepo>();
            mockUserRepo = new Mock<IUserRepo>();
            mockMapper = new Mock<IMapper>();
        }
    }
}