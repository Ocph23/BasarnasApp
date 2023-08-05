using Microsoft.AspNetCore.Identity;

namespace BasarnasApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}