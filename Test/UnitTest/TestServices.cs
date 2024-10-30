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

    [Fact]
    public void CheckIfEmailAlreadyExists_ShouldCheckIfEmailAlreadyIsRegisteredInDatabase_ThenReturnTrueIfEmailExists()
    {
        //Arrange
        SignUpModel user = new SignUpModel { FirstName = "Test", LastName = "Testsson", Email = "test@test.se", Password = "Bytmig123!" };
        UserEntity userEntity = new UserEntity { FirstName = "Test", LastName = "Testsson", Email = "test@test.se" };
        _context.Users.Add(userEntity);
        _context.SaveChanges();
        IUserServices userServices = new UserServices(_context);

        //Act
        var result = userServices.CheckIfEmailExistsAsync(user.Email);

        //Assert
        Assert.True(result);
    }
}
