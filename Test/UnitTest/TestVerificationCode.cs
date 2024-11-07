using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entities; 
using Data.Context;  
using Data.Services;

public class VerificationProviderTests
{
    private readonly DBContext _context;
    private readonly VerificationServices _verificationServices;

    public VerificationProviderTests()
    {
       
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}")
            .Options;

        _context = new DBContext(options);
        _verificationServices = new VerificationServices(_context); 
    }

    [Fact]
    public async Task VerifyCode_ShouldConfirmEmail_WhenUserExists()
    {
        // Arrange
        var email = "test@example.com";
        var testUser = new UserEntity { Email = email, EmailConfirmed = false };

      
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync();

        // Act
        var result = await _verificationServices.VerifyCode(email);

        // Assert
        Assert.True(result); 
        Assert.True(testUser.EmailConfirmed); 
    }
}
