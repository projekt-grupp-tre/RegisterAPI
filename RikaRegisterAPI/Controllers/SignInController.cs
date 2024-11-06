using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
                if(result.Succeeded)
                {
                    var userBasicUserInfo = await _userManager.FindByEmailAsync(signInModel.Email);
                    if (userBasicUserInfo != null)
                    {
                        return Ok(new
                        {
                            id = userBasicUserInfo.Id,
                            firstName = userBasicUserInfo.FirstName,
                            lastName = userBasicUserInfo.LastName,
                            email = userBasicUserInfo.Email,
                            imageUrl = userBasicUserInfo.ImageUrl ?? "default-profile-picture.jpg",
                        });
                    }
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
