using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Http;

namespace AuthZSample
{
    public static class Constants 
    {
        public const string WebsiteReadPolicy = "web-app-read";
    }
    
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
            services.AddMvc();
         
            services.ConfigureAuthorization(authzOptions => 
            {
                authzOptions.AddPolicy(Constants.WebsiteReadPolicy, policy => 
                {
                    policy.ActiveAuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireClaim(System.Security.Claims.ClaimTypes.NameIdentifier);
                    policy.RequireClaim("scope", "read");
                });
            });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
        
            // Configure the HTTP request pipeline.
        
            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }
            
            app.UseCookieAuthentication(options => 
            {
                options.LoginPath = new PathString("/membership/login");
            });
            
            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    
    // see: https://github.com/aspnet/Mvc/tree/85bb33a62a06fe8767d2df1c519a1cab78e7c9d1/test/WebSites/FiltersWebSite
    // see: https://github.com/aspnet/Mvc/blob/85bb33a62a06fe8767d2df1c519a1cab78e7c9d1/test/WebSites/FiltersWebSite/Startup.cs
    
    // Notes:
    
    // 1-) When you configure authz policy and try to protect a controller or action with
    //     AuthorizeAttribute, you will get "InvalidOperationException: The following authentication scheme was not accepted: {name-of-the-schema}"
    //     if you don't have an auth handler to handle that schema. This work is being done by 
    //     https://github.com/aspnet/HttpAbstractions/blob/67803d2f419c067cc9043ad26173bceea2467d89/src/Microsoft.AspNet.Http/Authentication/DefaultAuthenticationManager.cs by default.
    //     In above case, we have app.UseCookieAuthentication(); and policy.ActiveAuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
    //     to make it work nicely. 
    //     refer: http://stackoverflow.com/questions/31998052/asp-net-5-oauthbearerauthentication-the-following-authentication-scheme-was-not
    
    // 2-) Cookies auth redirects by default to your login page which is Account/Login?ReturnUrl={stuff}
    
    // 3-) After doing services.ConfigureAuthorization, UseStaticFiles thing is returning 404 for all static files.
    
    // 4-) Q: What is the diff between AddAuthorization and ConfigureAuthorization?
    //     A?: Could be https://github.com/aspnet/Security/blob/0f06b6a09a38d8027e1d181a07dbbe26ce36517e/src/Microsoft.AspNet.Authorization/ServiceCollectionExtensions.cs#L16
    //         it could be something which is being invoked by hosting stuff.
}