using Data.Context;
using Data.Interfaces;
using System.Diagnostics;

namespace Data.Services;

public class UserServices(DBContext dbContext) : IUserServices
{
    private readonly DBContext _dbContext = dbContext;

    public bool CheckIfEmailExistsAsync(string email)
    {
        try
        {
            if (_dbContext.Users.FirstOrDefault(x => x.Email == email) != null)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("CheckIfEmailExistsAsync", ex.Message);
        }

        return false;
    }
}
