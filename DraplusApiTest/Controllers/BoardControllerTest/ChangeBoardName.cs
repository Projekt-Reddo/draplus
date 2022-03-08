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
    public class ChangeBoardName : TestableBoardController
    {
        [Test]
        public async Task ChangeName_BoardNotExist_ReturnsNotFound()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardForChangeNameDto = new BoardForChangeNameDto
            {
                Id = "this is not a valid id",
                Name = "new name"
            };

            // Act
            var result = await boardController.UpdateBoardName("this is not a valid id" , boardForChangeNameDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        }

        [Test]
        public async Task ChangeName_EmptyStringName_ReturnsBadRequest()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardForChangeNameDto = new BoardForChangeNameDto
            {
                Id = "620779bc736e565a8aaafbae",
                Name = ""
            };

            mockBoardRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<Board>>()))
                .ReturnsAsync(new Board());

            mockBoardRepo.Setup(x => x.Update("620779bc736e565a8aaafbae", It.IsAny<Board>()))
                .ReturnsAsync(false);

            // Act
            var result = await boardController.UpdateBoardName("620779bc736e565a8aaafbae" , boardForChangeNameDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result.Result);
        }

        [Test]
        public async Task ChangeName_NormalFlow_ReturnsOK()
        {
            // Arrange
            var boardController = new BoardController(mockBoardRepo.Object, mockUserRepo.Object, mockMapper.Object);
            var boardForChangeNameDto = new BoardForChangeNameDto
            {
                Id = "620779bc736e565a8aaafbae",
                Name = "hIRyS"
            };

            mockBoardRepo.Setup(x => x.GetByCondition(It.IsAny<FilterDefinition<Board>>()))
                .ReturnsAsync(new Board());

            mockBoardRepo.Setup(x => x.Update("620779bc736e565a8aaafbae", It.IsAny<Board>()))
                .ReturnsAsync(true);

            // Act
            var result = await boardController.UpdateBoardName("620779bc736e565a8aaafbae" , boardForChangeNameDto);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<ResponseDto>), result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        }
    }
}