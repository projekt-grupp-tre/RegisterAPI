using Moq;
using Data.Interfaces;
using Data.Models;

namespace Test.UnitTest;

public class TestRepositories
{
    private Mock<IUserRepository> _userRepository;

    public TestRepositories()
    {
        _userRepository = new Mock<IUserRepository>();
    }

    [Fact]
    public async void AddUserToDatabaseAsync_ShouldAddAnUserToDatabase_ThenReturnUser()
    {
        //Arrange
        SignUpModel user = new SignUpModel { FirstName = "Test", LastName = "Testsson", Email = "test@test.se", Password = "Bytmig123!" };
        StatusModel status = new StatusModel { StatusCode = 201, Message = "User is added" };
        _userRepository.Setup(x => x.AddUserToDatabaseAsync(It.IsAny<SignUpModel>())).Returns(Task.FromResult(status));

        //Act
        var result = await _userRepository.Object.AddUserToDatabaseAsync(user);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(status.StatusCode, result.StatusCode);
    }
}
