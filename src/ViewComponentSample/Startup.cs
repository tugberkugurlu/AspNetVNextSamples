using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace ViewComponentSample 
{
    public class Startup 
    {
        public void Configure(IBuilder app)
        {
            app.UseErrorPage();
            app.UseFileServer();
            
            app.UseServices(services => 
            {
                services.AddMvc();
            });
            
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    "controllerActionRoute",
                    "{controller}/{action}",
                    new { controller = "Home", action = "Index" });
            });
        }
    }
}