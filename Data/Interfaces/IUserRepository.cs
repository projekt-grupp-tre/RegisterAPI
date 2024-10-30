using Data.Entities;
using Data.Models;

namespace Data.Interfaces;

public interface IUserRepository
{
    Task<StatusModel> AddUserToDatabaseAsync(SignUpModel model);
}
