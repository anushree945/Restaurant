using Microsoft.AspNetCore.Identity;
using Restaurant.Services.Identity.DbContexts;
using Restaurant.Services.Identity.Pages.Application;

namespace Restaurant.Services.Identity.Initializer
{
    public interface IDbInitializer
    {
        public Task InitializeAsync();
    }
}
