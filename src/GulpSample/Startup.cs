using Microsoft.AspNet.Builder;

namespace GulpSample
{    
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
			app.UseFileServer();
			app.UseErrorPage();
            app.UseWelcomePage();
        }
    }
}