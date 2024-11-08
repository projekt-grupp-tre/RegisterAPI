using Data.Entities;
using Data.Factories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RikaRegisterAPI.Controllers;

namespace Test.UnitTest
{
    public class TestController
    {
        //private SignInModel CreateASignInModelRequest()
        //{
        //    return new SignInModel
        //    {
        //        Email = "test@test.se",
        //        Password = "Bytmig123!",
        //        RememberMe = true
        //    };
        //}

        //private SignInModel CreateAIncorrectSignInModelRequest()
        //{
        //    return new SignInModel
        //    {
        //        Email = "",
        //        Password = "Bytmig123!",
        //        RememberMe = true
        //    };
        //}

        //private readonly Mock<UserManager<UserEntity>> _userManagerMock;
        //private readonly Mock<SignInManager<UserEntity>> _signInManagerMock;
        //private readonly GenerateJwtTokenFactory _generateJwtTokenFactory;
        //private readonly SignInController _controller;
        //private readonly IConfiguration _configuration;

        //public TestController()
        //{
        //    var userStoreMock = new Mock<IUserStore<UserEntity>>();

        //    _userManagerMock = new Mock<UserManager<UserEntity>>(
        //        userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        //    var contextAccessorMock = new Mock<IHttpContextAccessor>();
        //    var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();

        //    _signInManagerMock = new Mock<SignInManager<UserEntity>>(
        //        _userManagerMock.Object,
        //        contextAccessorMock.Object,
        //        userClaimsPrincipalFactoryMock.Object,
        //        null!, null!, null!, null!);


        //    _configuration = new ConfigurationBuilder()
        //    .AddInMemoryCollection(new Dictionary<string, string>
        //    {
        //        { "Jwt:Key", "TestSecretKey" },
        //        { "Jwt:Issuer", "TestIssuer" },
        //        { "Jwt:Audience", "TestAudience" }
        //    })
        //    .Build();

        //    _generateJwtTokenFactory = new GenerateJwtTokenFactory(_configuration);

        //    _controller = new SignInController(
        //        _userManagerMock.Object,
        //        _signInManagerMock.Object,
        //        _generateJwtTokenFactory);
        //}

        //[Fact]
        //public async Task SignInAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        //{
        //    //Arrange
        //    var signInModel = CreateAIncorrectSignInModelRequest();
        //    _controller.ModelState.AddModelError("Email", "You must enter an email");

        //    //Act
        //    var result = await _controller.SignInAsync(signInModel);

        //    //Assert
        //    Assert.IsType<BadRequestResult>(result);
        //}
    }
}
