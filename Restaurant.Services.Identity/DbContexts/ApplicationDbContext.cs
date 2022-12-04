using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Identity.Pages.Application;

namespace Restaurant.Services.Identity.DbContexts
{
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
