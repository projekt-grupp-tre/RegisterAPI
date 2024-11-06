

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

        public async Task<bool> VerifyCode(string email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return false;
            }
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }
            else 
            {
                await _dbcontext.SaveChangesAsync();
                return true;
            }
        }
    }
}


