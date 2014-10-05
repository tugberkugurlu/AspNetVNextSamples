using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace ViewComponentSample 
{
    public class Startup 
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();
            app.UseFileServer();
            
            app.UseServices(services => 
            {
                services.AddMvc();
                services.AddScoped<IProfileLinkManager, ProfileLinkManager>();
            });
            
            app.UseMvc(routes => 
            {
                routes.MapRoute("defaultRoute", "{controller=Home}/{action=Index}");
            });
        }
    }
}