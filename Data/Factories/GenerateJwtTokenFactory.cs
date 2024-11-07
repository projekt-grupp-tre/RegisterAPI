﻿using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Data.Factories;

public class GenerateJwtTokenFactory
{
    //private readonly IConfiguration _configuration;

    //public GenerateJwtTokenFactory(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public string GenerateJwtToken(UserEntity userEntity)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id),
            new Claim(JwtRegisteredClaimNames.Email, userEntity.Email!),
            new Claim("firstName", userEntity.FirstName),
            new Claim("lastName", userEntity.LastName),
            new Claim("imageUrl", userEntity.ImageUrl ?? "default-profile-picture.jpg" )
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b215a3db-7f30-4584-a2a2-de476e4de617"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7286",
            audience: "https://localhost:7259",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}