using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.HttpFeature;
using System.Threading.Tasks;
using System;

namespace RemoteIPAddressSample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (ctx) =>
            {
                await ctx.Response.WriteAsync("IP Address: " + ctx.GetClientIPAddress());
            });
        }
    }

    public static class HttpContextExtensions
    {
        public static string GetClientIPAddress(this HttpContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context");
            }

            // https://github.com/aspnet/HttpAbstractions/blob/fee220569aa108078ab0e231080724eb74ec8b2d/src/Microsoft.AspNet.Http/HttpContext.cs#L45-L48
            // https://github.com/aspnet/HttpAbstractions/blob/fee220569aa108078ab0e231080724eb74ec8b2d/src/Microsoft.AspNet.HttpFeature/IHttpConnectionFeature.cs
            IHttpConnectionFeature connection = context.GetFeature<IHttpConnectionFeature>();

            return connection != null
                ? connection.RemoteIpAddress.ToString()
                : null;
        }
    }
}