using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Http;

public class Startup 
{
    public void Configure(IBuilder app)
    {
        var config = new Configuration();
        config.AddEnvironmentVariables();
        
        app.Run(async ctx => 
        {
            ctx.Response.ContentType = "text/plain";
            DumpConfig(ctx.Response, config);
        });
    }
    
    private static async Task DumpConfig(HttpResponse response, IConfiguration config, string indentation = "")
    {
        foreach (var child in config.GetSubKeys())
        {
            await response.WriteAsync(indentation + "[" + child.Key + "] " + config.Get(child.Key) + "\r\n");
            await DumpConfig(response, child.Value, indentation + "  ");
        }
    }
}