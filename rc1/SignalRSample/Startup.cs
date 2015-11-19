using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Builder;
using Owin;

namespace SignalRSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var builder = new AppBuilder();
                    var hubConfig = new HubConfiguration { EnableDetailedErrors = true };

                    builder.MapSignalR(hubConfig);

                    var appFunc = builder.Build(typeof(Func<IDictionary<string, object>, Task>)) as Func<IDictionary<string, object>, Task>;

                    return appFunc;
                });
            });
        }
    }
}