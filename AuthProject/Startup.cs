using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using AuthProject.Services;
using Microsoft.AspNetCore.Authorization;
using AuthProject.Web.Authorization;

namespace AuthProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<ApplicationUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //})

            //   .AddEntityFrameworkStores<ApplicationDbContext>();
           // services.AddControllersWithViews();
           services.AddControllersWithViews(o=>o.Filters.Add(new AuthorizeFilter()));
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.Authority = "https://localhost:44319/";
                    options.ClientId = "AuthProject_web";

                    //Store in application secrets
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    options.CallbackPath = "/signin-oidc";

                    options.Scope.Add("AuthProject");
                    options.Scope.Add("AuthProject_API");
                    //options.Scope.Add("offline_access");// to provide refresh tokens 

                    options.SaveTokens = true;// used for API

                    //Default claims are mapped automtically but u need only to map custom claims 
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClaimActions.MapUniqueJsonKey("CareerStarted",
                        "CareerStarted");
                    options.ClaimActions.MapUniqueJsonKey("FullName", "FullName");
                    options.ClaimActions.MapUniqueJsonKey("Role", "role");
                    options.ClaimActions.MapUniqueJsonKey("Permission", "Permission");

                    options.ResponseType = "code";
                    options.ResponseMode = "form_post";// means reponse in request body(payload) instead of query string it much safier

                    //every time client autherize via Identity server it generates secret and identity server rememeber the secret belongs to the issued code
                    //PKCE solve issue of Code Substitution Attack
                    //PKCE is an extension to the Authorization Code flow to prevent certain attacks and to be able to securely perform the OAuth exchange from public clients.
                    options.UsePkce = true;


                    options.TokenValidationParameters.RoleClaimType = "Role";
                });
            
            services.AddHttpContextAccessor();
            services.AddHttpClient<IAttendeApiService, AttendeApiService>(
               async (services, client) =>
               {
                   var accessor = services.GetRequiredService<IHttpContextAccessor>();
                   var accessToken = await accessor.HttpContext
                        .GetTokenAsync("access_token");
                   client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("bearer", accessToken);
                   client.BaseAddress = new Uri("https://localhost:44316/");
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsSpeaker", policy => policy.RequireRole("Speaker"));
                options.AddPolicy("CanAddConference", policy => policy.RequireClaim("Permission", "AddConference"));
                options.AddPolicy("YearsOfExperience", policy => policy.AddRequirements(new YearsOfExperienceRequirement(30)));
                options.AddPolicy("CanEditProposal", policy => policy.AddRequirements(new ProposalRequirement()));

            });

            services.AddSingleton<IAuthorizationHandler, YearsOfExperienceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProposalApprovedAuthorizationHandler>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conference}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
