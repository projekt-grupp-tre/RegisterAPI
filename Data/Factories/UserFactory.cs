using Data.Entities;
using Data.Models;

namespace Data.Factories;

public static class UserFactory
{
    public static UserEntity Create(SignUpModel signUpModel)
    {
        return new UserEntity
        {
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName,
            Email = signUpModel.Email,
            PasswordHash = signUpModel.Password,
        };
    }
}
