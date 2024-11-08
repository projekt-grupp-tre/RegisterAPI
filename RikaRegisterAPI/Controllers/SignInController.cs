using Data.Entities;
using Data.Factories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using System.Diagnostics;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, GenerateJwtTokenFactory tokenFactory) : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly GenerateJwtTokenFactory _tokenFactory = tokenFactory;

        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            try
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
                            });
                        }
                    }

                    return Unauthorized("User not found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return BadRequest("Invalid Data");
        }
    }
}
