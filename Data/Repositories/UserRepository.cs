using Data.Context;
using Data.Entities;
using Data.Factories;
using Data.Interfaces;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class UserRepository(DBContext context, UserServices userServices, UserManager<UserEntity> userManager) : IUserRepository
{
    private readonly DBContext _context = context;
    private readonly UserServices _userServices = userServices;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<StatusModel> AddUserToDatabaseAsync(SignUpModel model)
    {
        try
        {
            if (model != null)
            {
                if(!_userServices.CheckIfEmailExistsAsync(model.Email))
                {
                    UserEntity userEntity = UserFactory.Create(model);
                    //var result = await _context.Users.AddAsync(userEntity);
                    var userResult = await _userManager.CreateAsync(userEntity, model.Password);

                    if(userResult.Succeeded)
                    {                       
                        await _context.SaveChangesAsync();
                        return new StatusModel { StatusCode = 201, Message = "User is successfully created" };
                    }
                }
                return new StatusModel { StatusCode = 409, Message = "Email already exists" };
            }
            return new StatusModel { StatusCode = 400, Message = "" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine("AddUserToDatabaseAsync", ex.Message);
        }
        return new StatusModel { StatusCode = 500, Message = "Internal server error" };
    }

    public async Task<UserEntity> GetUserEntityAsync(string email)
    {
        try
        {
            if (email != null)
            {
                var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (userEntity != null)
                {
                    return userEntity;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetUserFromDatabaseAsync", ex.Message);
        }

        return null!;
    }
}
