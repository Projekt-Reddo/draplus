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
    public class DeleteBoard : TestableBoardController
    {
        [Test]
        public async Task DeleteBoard_IdIsNull_ReturnsBadRequest()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            string boardId = null;

            // Act
            var result = await boardController.DeleteBoard(boardId);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
        }
        
        [Test]
        public async Task DeleteBoard_IdIsValid_ReturnsOk()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            string boardId = "6213a6454577874737d929a8";

            // Act
            mockBoardRepo.Setup(x => x.Delete("6213a6454577874737d929a8")).ReturnsAsync(true); 
            var result = await boardController.DeleteBoard(boardId);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        }
    }
}