using Microsoft.AspNetCore.Identity;

namespace PustokMvcApp.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
