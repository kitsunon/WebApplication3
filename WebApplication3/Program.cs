using Microsoft.AspNetCore.Identity;
using WebApplication3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(); //save session in memory
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Lockout settings
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3;

    // Other Identity options...
}).AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddDistributedMemoryCache(); // Add this line for distributed memory cache
builder.Services.AddSession(); // Add this line for session
builder.Services.AddDataProtection();

builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
    Config.ExpireTimeSpan = TimeSpan.FromMinutes(1); // Set your desired session timeout here
    Config.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            // If it's an AJAX request, send a 403 status code
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                // Redirect to the login page after session timeout
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession(); // Place UseSession after UseRouting and before UseAuthorization

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.MapRazorPages();

app.Run();
