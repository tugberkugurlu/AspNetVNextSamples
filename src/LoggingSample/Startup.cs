using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using System;

namespace LoggingSample
{
    public static class Constants 
    {
        public const string LoggerName = "LoggingSample";
    }
    
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
            ILogger _logger = _loggerFactory.Create(typeof(Startup).FullName);
            AddProviders(_loggerFactory);
            
            _logger.WriteInformation("Starting");
            
            app.UseMiddleware(typeof(LoggerScopeMiddleware));
            app.UseMiddleware(typeof(MyPassThroughMiddleware));
            app.UseMiddleware(typeof(MyMiddleware), "Yo");
        }
        
        private void AddProviders(ILoggerFactory loggerFactory)
        {
            // providers may be added to an ILoggerFactory at any time, existing ILoggers are updated
#if !ASPNETCORE50
            loggerFactory.AddNLog(new global::NLog.LogFactory());
#endif
            loggerFactory.AddConsole();
    
        }
    }
    
    public class LoggerScopeMiddleware 
    {
        private RequestDelegate _next;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        
        public LoggerScopeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }
            
            _next = next;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.Create(Constants.LoggerName);
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            using(_logger.BeginScope(Guid.NewGuid().ToString()))
            {
                _logger.WriteInformation("LoggerScopeMiddleware: started the scope!");
                await _next(httpContext);
            }
        }
    }
    
    public class MyPassThroughMiddleware
    {
        private RequestDelegate _next;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        
        public MyPassThroughMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }
            
            _next = next;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.Create(Constants.LoggerName);
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            _logger.WriteInformation("Getting in...");
            
            try
            {
                await _next(httpContext);
            }
            finally
            {
                _logger.WriteInformation("Getting out...");
            }
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
            _logger = loggerFactory.Create(Constants.LoggerName);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.WriteInformation(_greeting + ", middleware!\r\n");
            await httpContext.Response.WriteAsync(_greeting + ", middleware!\r\n");
            await httpContext.Response.WriteAsync("This request is a " + httpContext.Request.Method + "\r\n");
        }
    }
}