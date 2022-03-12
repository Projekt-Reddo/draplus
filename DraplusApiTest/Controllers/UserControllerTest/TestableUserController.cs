using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Helpers;
using Google.Apis.Auth;
using Moq;
using NUnit.Framework;

namespace DraplusApiTest.Controllers.UserControllerTest
{
    public class TestableUserController
    {
        public Mock<IBoardRepo> mockBoardRepo { get; set; } = null!;
        public Mock<IUserRepo> mockUserRepo { get; set; } = null!;
        public Mock<IMapper> mockMapper { get; set; } = null!;
        public Mock<IJwtGenerator> mockJwtGenerator { get; set; } = null!;
        public Mock<GoogleJsonWebSignature> mockGoogleJson { get; set; } = null!;


        [SetUp]
        public void Setup()
        {
            // Mock controller dependencies
            mockBoardRepo = new Mock<IBoardRepo>();
            mockUserRepo = new Mock<IUserRepo>();
            mockMapper = new Mock<IMapper>();
            mockJwtGenerator = new Mock<IJwtGenerator>();
            mockGoogleJson = new Mock<GoogleJsonWebSignature>();
        }   
    }
}