using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Data.Factories;

public class GenerateJwtTokenFactory
{
    private readonly IConfiguration _configuration;

    public GenerateJwtTokenFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(UserEntity userEntity)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id),
            new Claim(JwtRegisteredClaimNames.Email, userEntity.Email!),
            new Claim("firstName", userEntity.FirstName ?? ""),
            new Claim("lastName", userEntity.LastName ?? ""),
            new Claim("imageUrl", userEntity.ImageUrl ?? "default-profile-picture.jpg" ),
            new Claim("address", userEntity.Address ?? ""),
            new Claim("city", userEntity.City ?? ""),
            new Claim("postalCode", userEntity.PostalCode ?? ""),
            new Claim("country", userEntity.Country ?? ""),
            new Claim("age", userEntity.Age.ToString() ?? ""),
            new Claim("gender", userEntity.GenderId.ToString() ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Values:JwtSecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
