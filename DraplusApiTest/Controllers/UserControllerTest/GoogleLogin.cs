using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DraplusApi.Controllers;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace DraplusApiTest.Controllers.UserControllerTest
{
    public class GoogleLogin : TestableUserController
    {
        [Test]
        public async Task GoogleLogin_IdIsNull_ReturnsBadRequest()
        {
            // Arrange
            var authController = new AuthController(mockUserRepo.Object, mockBoardRepo.Object, mockMapper.Object, mockJwtGenerator.Object, mockSignInRepo.Object);
            var userView = new UserView
            {
                tokenId = "6213a6454577874737d929a8"
            };
            Payload payLoadTest = new Payload
            {
                Email = "email.com",
                EmailVerified = true,
            };
            User usetTest = new User
            {
                Email = "email.com",
                IsAdmin = false,
            };
            Claim[] claims = new Claim[]
            {

            };
            string tokenn = "ajwogp[waglpawgkp";
            //mockGoogleJson.Setup(x => x.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings())).ReturnsAsync(payLoadTest);
            mockUserRepo.Setup(x => x.Authenticate(payLoadTest)).ReturnsAsync((usetTest, true));
            mockBoardRepo.Setup(x => x.Add(It.IsAny<Board>())).ReturnsAsync(new Board());
            mockJwtGenerator.Setup(x => x.GenerateClaims(usetTest)).Returns(claims);
            mockJwtGenerator.Setup(x => x.GenerateJwtToken(claims)).Returns(tokenn);

            //Act
            var result = await authController.Google(userView);

            //Assert 
            Assert.Pass();
        }
    }
}