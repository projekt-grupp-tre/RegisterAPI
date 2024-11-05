using Data.Entities;
using Data.Factories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RikaRegisterAPI.Controllers;

namespace Test.UnitTest
{
    public class TestController
    {
        //private readonly Mock<UserManager<UserEntity>> _userManagerMock;
        //private readonly Mock<SignInManager<UserEntity>> _signInManagerMock;
        //private readonly SignInController _controller;

        //public TestController()
        //{
        //    _controller = new SignInController(_userManagerMock.Object, _signInManagerMock.Object);
        //}

        //[Fact]
        //public async void SignInAsync_ShouldLoginAnUser_ThenReturnOkResult()
        //{
        //    //Arrange
        //    var signInModel = new SignInModel { Email = "test@test.se", Password = "Oklart", RememberMe = false };
        //    var user = new UserEntity { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@test.se", ImageUrl = null, PasswordHash = "Oklart" };
        //    _signInManagerMock.Setup(x => x.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        //    //Act
        //    var result = await _controller.SignInAsync(signInModel);

        //    //Assert
        //    Assert.IsType<OkResult>(result);
        //}

        //[Fact]
        //public async void SignInAsync_ShouldNotLoginAnUser_ThenReturUnauthorizedResponse()
        //{
        //    //Arrange
        //    var signInModel = new SignInModel { Email = "test@test.se", Password = "Fellösenord", RememberMe = false };
        //    var user = new UserEntity { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@test.se", ImageUrl = null, PasswordHash = "Oklart" };
        //    _signInManagerMock.Setup(x => x.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        //    //Act
        //    var result = await _controller.SignInAsync(signInModel);

        //    //Assert
        //    Assert.IsType<UnauthorizedResult>(result);
        //}
    }
}
