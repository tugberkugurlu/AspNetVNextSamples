using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
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
#if DNX452
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .WriteTo.Elasticsearch()
#endif
                .MinimumLevel.Verbose()
                .CreateLogger();

            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddSerilog(serilogLogger);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<CarsContext, CarsContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<RequestIdMiddleware>();
            app.UseMiddleware<RequestUrlLoggerMiddleware>();
            app.UseMvc();
        }
    }

    public class RequestUrlLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.Framework.Logging.ILogger _logger;

        public RequestUrlLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestUrlLoggerMiddleware>();
        }

        public Task Invoke (HttpContext context)
        {
            _logger.LogInformation("{Method}: {Url}", context.Request.Method, context.Request.Path);
            return _next(context);
        }
    }

    public class RequestIdMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestIdFeature = context.Features.Get<IHttpRequestIdentifierFeature>();
            if (requestIdFeature?.TraceIdentifier != null)
            {
                context.Response.Headers["RequestId"] = requestIdFeature.TraceIdentifier;
            }

            await _next(context);
        }
    }
}