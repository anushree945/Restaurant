using Microsoft.AspNetCore.Identity;

namespace Restaurant.Services.Identity.Pages.Application
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
