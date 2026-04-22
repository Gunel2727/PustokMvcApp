


using Microsoft.Build.Framework;


namespace PustokMvcApp.Areas.Manage.ViewModels;

using System.ComponentModel.DataAnnotations;
    public class AdminLoginVm
    {
        [Required]
        [MinLength(2)]
        public string? Username { get; set; }
        [Required]
        [MinLength(2)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }    
    }

