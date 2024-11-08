using Data.Context;
using Data.Entities;
using Data.Factories;
using Data.Repositories;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb")));

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<DBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<GenerateJwtTokenFactory>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<VerificationServices>();
builder.Services.AddScoped<UserManager<UserEntity>>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Values:Jwt-Secret-key"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
