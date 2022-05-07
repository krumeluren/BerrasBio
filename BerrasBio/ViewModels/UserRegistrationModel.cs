using System.ComponentModel.DataAnnotations;

namespace BerrasBio.ViewModels;

public class UserRegistrationModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email krävs")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Lösenord krävs")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Lösenordet stämmer inte överens.")]
    public string ConfirmPassword { get; set; }
}
