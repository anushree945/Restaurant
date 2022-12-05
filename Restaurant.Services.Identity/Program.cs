using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Identity;
using Restaurant.Services.Identity.DbContexts;
using Restaurant.Services.Identity.Initializer;
using Restaurant.Services.Identity.Pages.Application;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var identityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;

}).AddInMemoryIdentityResources(Details.IdentityResources)
.AddInMemoryApiScopes(Details.ApiScopes)
.AddInMemoryClients(Details.Clients)
.AddAspNetIdentity<ApplicationUser>;

builder.Services.AddScoped<IDbInitializer, DbInitializer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();
app.MapRazorPages();

var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
dbInitializer.InitializeAsync();

app.Run();



