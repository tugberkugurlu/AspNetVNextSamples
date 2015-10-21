using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace LoggingCorrelationSample
{
	public class Startup 
	{	
		public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
		}
		
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(minLevel: LogLevel.Verbose);
			
			app.Use(async (ctx, next) => 
			{
				var logger = loggerFactory.CreateLogger("temp");
				using(logger.BeginScope(Guid.NewGuid().ToString()))
				{
					await next();	
				}
			});
			
			app.UseMvc();
		}
	}
	
	public static class LoggerExtensions
	{
	}
}