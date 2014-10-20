using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using System;

namespace LoggingSample
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        
        public Startup(ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }
            
            _loggerFactory = loggerFactory;
        }
        
        public void Configure(IApplicationBuilder app)
        {
            _loggerFactory.AddConsole();
            ILogger _logger = _loggerFactory.Create(typeof(Startup).FullName);
            _logger.WriteInformation("Starting");
            
            app.UseMiddleware(typeof(MyMiddleware), "Yo");
        }
    }
    
    public class MyMiddleware
    {
        private RequestDelegate _next;
        private string _greeting;
        private IServiceProvider _services;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        
        public MyMiddleware(RequestDelegate next, string greeting, IServiceProvider services, ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }
            
            _next = next;
            _greeting = greeting;
            _services = services;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.Create<MyMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.WriteInformation(_greeting + ", middleware!\r\n");
            await httpContext.Response.WriteAsync(_greeting + ", middleware!\r\n");
            await httpContext.Response.WriteAsync("This request is a " + httpContext.Request.Method + "\r\n");
        }
    }
}