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
    public class AddBoard : TestableBoardController
    {
        [Test]
        public async Task AddBoard_UserIdIsNull_ReturnsBadRequest()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardCreateDto = new BoardCreateDto
            {
                UserId = null!,
            };

            // Act
            var result = await boardController.AddBoard(boardCreateDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
        }

        [Test]
        public async Task AddBoard_UserIdIsNotExisted_ReturnsNotFound()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardCreateDto = new BoardCreateDto
            {
                UserId = "6213a6454577874737d929a8",
            };

            mockUserRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<User>>()))
                .ReturnsAsync((User)null!); // Cast for not ambious nullable

            // Act
            var result = await boardController.AddBoard(boardCreateDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task AddBoard_UserIdIsNotNull_ReturnsOk()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardCreateDto = new BoardCreateDto
            {
                UserId = "6213a6454577874737d929a8",
            };

            mockUserRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<User>>()))
                .ReturnsAsync(new User());

            mockBoardRepo.Setup(x => x.Add(It.IsAny<Board>()))
                .ReturnsAsync(new Board());

            // Act
            var result = await boardController.AddBoard(boardCreateDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        }
    }
}