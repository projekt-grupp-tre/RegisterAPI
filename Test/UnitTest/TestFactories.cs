using Data.Entities;
using Data.Factories;
using Data.Models;
using Moq;

namespace Test.UnitTest;

public class TestFactories
{
    private readonly GenerateJwtTokenFactory _factory;

    public TestFactories()
    {
        _factory = new GenerateJwtTokenFactory();
    }

    [Fact]
    public void ShouldConvertASignUpModelToUserEntity_ThenReturnUserEntity()
    {
        //Arrange
        var signUpModel = new SignUpModel { Email = "test@test.se", FirstName = "Test", LastName = "Testsson", Password = "Oklart" };


        //Act
        var result = UserFactory.Create(signUpModel);

        //Assert
        Assert.Equal(result.Email, signUpModel.Email);
        Assert.Equal(result.FirstName, signUpModel.FirstName);
        Assert.Equal(result.LastName, signUpModel.LastName);
    }

    [Fact]
    public void ShouldGenerateAJwtToken_ThenReturnAencryptedString()
    {
        //Arrange
        var userEntity = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test@test.se",
                Email = "test@test.se",
                FirstName = "Test",
                LastName = "Testsson",
            };

        //Act
        var result = _factory.GenerateJwtToken(userEntity);

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
