using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Data.Context;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Identity;

namespace TestVerificationCode
{
    public class VerificationProviderTests
    {
        private readonly DBContext _context;
        private readonly VerificationServices _verificationService;
        private readonly Mock<UserManager<UserEntity>> _mockUserManager;

        public VerificationProviderTests()
        {
           
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}")
                .Options;

            _context = new DBContext(options);

           
            _mockUserManager = new Mock<UserManager<UserEntity>>(
                Mock.Of<IUserStore<UserEntity>>(), null, null, null, null, null, null, null, null);

            
            _verificationService = new VerificationServices(_context, _mockUserManager.Object);
        }

        [Fact]
        public async Task VerifyCode_ShouldConfirmEmail_WhenUserExists()
        {
            // Arrange
            var email = "test@example.com";
            var testUser = new UserEntity
            {
                Email = email,
                EmailConfirmed = false,
                FirstName = "Test",
                LastName = "User"
            };

       
            _context.Users.Add(testUser);
            await _context.SaveChangesAsync();

            // Act
            var result = await _verificationService.VerifyCode(email);

            // Assert
            Assert.True(result); 
            Assert.True(testUser.EmailConfirmed); 
        }

      
    }
}
