using System.Collections.Generic;
using System.Threading.Tasks;
using DraplusApi.Controllers;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace DraplusApiTest.Controllers.BoardControllerTest
{
    public class GetUserBoards : TestableBoardController
    {
        [Test]
        public async Task GetUserBoards_UserNotExist_ReturnsNotFound()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            string userId = "this is not a valid id";

            mockUserRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<User>>()))
                .ReturnsAsync((User)null!);

            var filter = Builders<Board>.Filter.Eq("UserId", userId);

            mockBoardRepo.Setup(x => x.GetAll(filter, null, null, null, null))
                .ReturnsAsync((IEnumerable<Board>)null!);

            // Act
            var result = await boardController.GetUserBoards(userId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task GetUserBoards_NormalFlow_ReturnsOkie()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            string userId = "6213a6454577874737d929a8";

            mockUserRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<User>>()))
                .ReturnsAsync(new User { Id = userId });

            var filter = Builders<Board>.Filter.Eq("UserId", userId);

            mockBoardRepo.Setup(x => x.GetAll(filter, null, null, null, null))
                .ReturnsAsync(new List<Board>());

            // Act
            var result = await boardController.GetUserBoards(userId);

            // Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        }
    }
}