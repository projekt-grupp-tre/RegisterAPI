using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserController(UserRepository _userRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            if (email != null)
            {
                var userEntity = await _userRepository.GetUserEntityAsync(email);
                return userEntity;
            }

            return null!;
        }
    }
}
