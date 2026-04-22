using Microsoft.Build.Framework;


namespace PustokMvcApp.ViewModels.UserVm;
using System.ComponentModel.DataAnnotations;
public class RegisterVm
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [MinLength(2)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

