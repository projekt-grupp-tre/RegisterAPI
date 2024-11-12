

using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class VerificationServices(DBContext dbcontext, UserManager<UserEntity> userManager)
    {
        private readonly DBContext _dbcontext = dbcontext;
        private readonly UserManager<UserEntity> _userManager=userManager;

       

        public DBContext Context { get; }

        public async Task<bool> VerifyCode(string email)
        {
            try
            {
                var userToBeUpdated = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (userToBeUpdated != null)
                {
                    UserEntity updatedUser = userToBeUpdated;
                    updatedUser.EmailConfirmed = true;
                    _dbcontext.Entry(userToBeUpdated).CurrentValues.SetValues(updatedUser);
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}


