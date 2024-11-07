using Data.Factories;
using Data.Models;
using Moq;

namespace Test.UnitTest;

public class TestFactories
{

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
        Assert.Equal(result.PasswordHash, signUpModel.Password);
    }
}
