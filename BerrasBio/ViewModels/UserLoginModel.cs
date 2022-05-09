﻿



using System.ComponentModel.DataAnnotations;

namespace BerrasBio.ViewModels
{
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
