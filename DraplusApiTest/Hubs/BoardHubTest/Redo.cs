using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DraplusApi.Dtos;
using DraplusApi.Hubs;
using Moq;
using NUnit.Framework;
using static Constant;

namespace DraplusApiTest.Hubs.BoardHubTest
{
    public class Redo : TestableBoardHub
    {
        [Test]
        public async Task Redo_shapeExist_Success()
        {
            // Arrange
            var boardHub = new BoardHub(
                connections: mockConnections.Object,
                boardRepo: mockBoardRepo.Object,
                mapper: mockMapper.Object,
                userRepo: mockUserRepo.Object,
                shapeList: mockShapeList.Object,
                noteList: mockNoteList.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };
            mockConnections.Setup(connections => connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true); // ! after userConnection is for null forgiving

            mockShapeList.Setup(list => list[It.IsAny<string>()]).Returns(new List<ShapeReadDto>() {
                new ShapeReadDto() {Id = "1", ClassName = "LinePath", Data = ""},
                new ShapeReadDto() {Id = "2", ClassName = "Text", Data = ""},
                new ShapeReadDto() {Id = "3", ClassName = "Eraser", Data = ""},
                new ShapeReadDto() {Id = "4", ClassName = "LinePath", Data = ""},
            }); // ! after note is for null forgiving

            var shape = new ShapeReadDto { Id = "4", ClassName = "LinePath", Data = "" };

            // Act
            await boardHub.Redo(shape);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveShape, new object[] { shape }, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Redo_shapeNotExist_Success()
        {
            // Arrange
            var boardHub = new BoardHub(
                connections: mockConnections.Object,
                boardRepo: mockBoardRepo.Object,
                mapper: mockMapper.Object,
                userRepo: mockUserRepo.Object,
                shapeList: mockShapeList.Object,
                noteList: mockNoteList.Object);
            AssignToHubRequiredProperties(boardHub); // Resolve hub dependencies as IClientsProxy...

            UserConnection userConnection = new UserConnection { Board = "board1" };
            mockConnections.Setup(connections => connections.TryGetValue(It.IsAny<string>(), out userConnection!)).Returns(true); // ! after userConnection is for null forgiving

            mockShapeList.Setup(list => list[It.IsAny<string>()]).Returns(new List<ShapeReadDto>() {
                new ShapeReadDto() {Id = "1", ClassName = "LinePath", Data = ""},
                new ShapeReadDto() {Id = "2", ClassName = "Text", Data = ""},
                new ShapeReadDto() {Id = "3", ClassName = "Eraser", Data = ""},
                new ShapeReadDto() {Id = "4", ClassName = "LinePath", Data = ""},
            }); // ! after note is for null forgiving

            var shape = new ShapeReadDto { Id = "5", ClassName = "Text", Data = "" };

            // Act
            await boardHub.Redo(shape);

            // Assert
            ClientsOthersInGroupMock.Verify(x => x.SendCoreAsync(HubReturnMethod.ReceiveShape, new object[] { shape }, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}