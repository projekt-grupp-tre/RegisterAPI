using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthServices _authService;

        public AuthorizationController(AuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("tokens")]
        public async Task<ActionResult<TokenModel>> GenerateTokens(string userId)
        {
            var tokens = await _authService.GenerateTokens(userId);
            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenModel>> RefreshTokens(string userId, string refreshToken)
        {
            try
            {
                var tokens = await _authService.RefreshTokens(userId, refreshToken);
                return Ok(tokens);
            }
            catch (SecurityTokenException)
            {
                return BadRequest("Invalid refresh token");
            }
        }
    }
}
