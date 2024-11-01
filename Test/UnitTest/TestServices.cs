using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.UnitTest;

public class TestServices
{

    private readonly DBContext _context = new(new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
    private readonly Mock<IUserServices> _userServices;

    public TestServices()
    {
        _userServices = new Mock<IUserServices>();
    }

    [Fact]
    public void CheckIfEmailAlreadyExists_ShouldCheckIfEmailAlreadyIsRegisteredInDatabase_ThenReturnTrueIfEmailExists()
    {
        //Arrange
        SignUpModel user = new SignUpModel { FirstName = "Test", LastName = "Testsson", Email = "test@test.se", Password = "Bytmig123!" };
        _userServices.Setup(x => x.CheckIfEmailExistsAsync(user.Email)).Returns(true);

        //Act
        var result = _userServices.Object.CheckIfEmailExistsAsync(user.Email);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckIfEmailAlreadyExists_ShouldCheckIfEmailAlreadyIsRegisteredInDatabase_ThenReturnFalseIfEmailIsUnique()
    {
        //Arrange
        SignUpModel user = new SignUpModel { FirstName = "Test", LastName = "Testsson", Email = "test@test.se", Password = "Bytmig123!" };
        _userServices.Setup(x => x.CheckIfEmailExistsAsync(user.Email)).Returns(false);

        //Act
        var result = _userServices.Object.CheckIfEmailExistsAsync(user.Email);

        //Assert
        Assert.False(result);
    }
}
