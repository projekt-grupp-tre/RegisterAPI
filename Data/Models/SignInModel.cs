using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class SignInModel
{
    [Required(ErrorMessage = "You must enter an email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a password")]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
