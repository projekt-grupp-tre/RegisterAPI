namespace Data.Interfaces;

public interface IUserServices
{
    bool CheckIfEmailExistsAsync(string email);
}