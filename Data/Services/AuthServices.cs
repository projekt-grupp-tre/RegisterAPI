using Data.Entities;
using Data.Factories;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security;

namespace Data.Services;

public class AuthServices
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserEntity> _userManager;
    private readonly GenerateJwtTokenFactory _generateJwtTokenFactory;
    private readonly UserRepository _userRepository;
    public AuthServices(IConfiguration configuration, UserManager<UserEntity> userManager, GenerateJwtTokenFactory generateJwtTokenFactory, UserRepository userRepository)
    {
        _configuration = configuration;
        _userManager = userManager;
        _generateJwtTokenFactory = generateJwtTokenFactory;
        _userRepository = userRepository;
    }

    public async Task<TokenModel> GenerateTokens(string userId)
    {
        var userEntity = await _userManager.FindByIdAsync(userId);
        var accessToken = _generateJwtTokenFactory.GenerateJwtToken(userEntity);
        var refreshToken = _generateJwtTokenFactory.GenerateRefreshToken();

        await SaveRefreshTokenToIdentity(userEntity, refreshToken);

        return new TokenModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Expires = DateTime.UtcNow.AddMinutes(20),
        };
    }

    private async Task SaveRefreshTokenToIdentity(UserEntity user, string refreshToken)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, "TokenProvider", "RefreshToken");

        await _userManager.SetAuthenticationTokenAsync(user, "TokenProvider", "RefreshToken", refreshToken);
    }

    public async Task<TokenModel> RefreshTokens(string userId, string refreshToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ArgumentException("Invalid user id", nameof(userId));

        // Hämta sparad refresh token från Identity
        var savedRefreshToken = await _userManager.GetAuthenticationTokenAsync(
            user,
            "TokenProvider",
            "RefreshToken"
        );

        if (savedRefreshToken != refreshToken)
            throw new SecurityTokenException("Invalid refresh token");

        // Generera nya tokens
        return await GenerateTokens(user.Id);
    }
}
