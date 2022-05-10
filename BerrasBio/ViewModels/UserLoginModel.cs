



using System.ComponentModel.DataAnnotations;

namespace BerrasBio.ViewModels
{
    /// <summary>
    /// ViewModel for the login form
    /// </summary>
    public class UserLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public UserLoginModel()
        {
                
        }
    }
}
