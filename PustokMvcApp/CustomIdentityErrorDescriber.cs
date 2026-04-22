using Microsoft.AspNetCore.Identity;

namespace PustokMvcApp
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"LoremPassword must contain at least one non-alphanumeric character."
            };
        }
    }
}
