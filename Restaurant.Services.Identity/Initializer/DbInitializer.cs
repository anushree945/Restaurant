using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Restaurant.Services.Identity.DbContexts;
using Restaurant.Services.Identity.Pages.Application;
using System.Security.Claims;

namespace Restaurant.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<DbInitializer> logger;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (roleManager.FindByNameAsync(Details.Admin).Result == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(Details.Admin));
                    await roleManager.CreateAsync(new IdentityRole(Details.Customer));
                }
                else
                {
                    return;
                }

                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@restaurant.com",
                    EmailConfirmed = true,
                    PhoneNumber = "9999999999",
                    FirstName = "Anushree",
                    LastName = "Sen"
                };
                ApplicationUser customerUser = new ApplicationUser()
                {
                    UserName = "customer",
                    Email = "customer@restaurant.com",
                    EmailConfirmed = true,
                    PhoneNumber = "9999999999",
                    FirstName = "Suman",
                    LastName = "Sen"
                };

                userManager.CreateAsync(adminUser, "P@ssw0rd").GetAwaiter().GetResult();
                userManager.CreateAsync(customerUser, "P@ssw0rd").GetAwaiter().GetResult();
                await userManager.AddToRoleAsync(adminUser, Details.Admin);
                await userManager.AddToRoleAsync(customerUser, Details.Customer);

                IEnumerable<Claim> adminClaims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                    new Claim(JwtClaimTypes.Role, Details.Admin)
                };
                IEnumerable<Claim> customerClaims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {customerUser.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                    new Claim(JwtClaimTypes.Role, Details.Customer)
                };

                await userManager.AddClaimsAsync(adminUser, adminClaims);
                await userManager.AddClaimsAsync(customerUser, customerClaims);
                logger.LogInformation("Roles, Users, Claims added to database");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while seeding users");
                throw;
            }
        }
    }
}
