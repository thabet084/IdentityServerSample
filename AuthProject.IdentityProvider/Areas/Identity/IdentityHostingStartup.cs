using System;
using AuthProject.IdentityProvider.Areas.Identity.Data;
using AuthProject.IdentityProvider.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AuthProject.IdentityProvider.Areas.Identity.IdentityHostingStartup))]
namespace AuthProject.IdentityProvider.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthWebContextConnection")));

                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<AuthWebContext>();

                services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddEntityFrameworkStores<AuthWebContext>()
                   .AddDefaultUI()
                   .AddDefaultTokenProviders();

                services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

                services.AddAuthentication()
                .AddGoogle(o =>
                {
                    o.ClientId = "686977813024-1pabqkfoar3btu6tsh7puhu3pogcivi0.apps.googleusercontent.com";
                    o.ClientSecret = context.Configuration["Google:ClientSecret"];
                });

            });
        }
    }
}