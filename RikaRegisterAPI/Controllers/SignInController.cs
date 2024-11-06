using Data.Entities;
using Data.Factories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, GenerateJwtTokenFactory tokenFactory) : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly GenerateJwtTokenFactory _tokenFactory;

        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(signInModel.Email);
                    if (user != null)
                    {
                        var token = _tokenFactory.GenerateJwtToken(user);
                        return Ok(new
                        {
                            jwttoken = token,
                            id = user.Id,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            email = user.Email,
                            imageUrl = user.ImageUrl ?? "default-profile-picture.jpg",
                        });
                    }
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
