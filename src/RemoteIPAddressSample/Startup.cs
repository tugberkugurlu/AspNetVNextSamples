using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.HttpFeature;
using System.Net;

namespace RemoteIPAddressSample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (ctx) =>
            {
                // https://github.com/aspnet/HttpAbstractions/blob/fee220569aa108078ab0e231080724eb74ec8b2d/src/Microsoft.AspNet.Http/HttpContext.cs#L45-L48
                // https://github.com/aspnet/HttpAbstractions/blob/fee220569aa108078ab0e231080724eb74ec8b2d/src/Microsoft.AspNet.HttpFeature/IHttpConnectionFeature.cs
                IHttpConnectionFeature connection = ctx.GetFeature<IHttpConnectionFeature>();

                // https://github.com/aspnet/HttpAbstractions/blob/fee220569aa108078ab0e231080724eb74ec8b2d/src/Microsoft.AspNet.HttpFeature/IHttpConnectionFeature.cs#L12
                // TODO: Should I do null check here?
                IPAddress remoteIpAddress = connection.RemoteIpAddress;

                await ctx.Response.WriteAsync("IP Address: " + remoteIpAddress.ToString());
            });
        }
    }
}