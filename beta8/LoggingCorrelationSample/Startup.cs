using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Serilog;

namespace LoggingCorrelationSample
{
    public class Startup
    {
        public Startup(ILoggerFactory loggerFactory)
        {
            var serilogLogger = new LoggerConfiguration()
                .WriteTo
                .TextWriter(Console.Out)
                .MinimumLevel.Verbose()
                .CreateLogger();

            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddSerilog(serilogLogger);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}