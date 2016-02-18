using System;
using Microsoft.Framework.Runtime.Common.CommandLine;

namespace AspNet5CommandLineSample
{
    public class Program
    {
        public int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "AspNet5CommandLineSample";
            app.FullName = app.Name;
            var pathArg = app.Argument("[path]", "Path to directory to be served through HTTP, default is current directory", c => 
            {
                c.Value = "bar";
            });
            var optionWatch = app.Option("--watch", "Watch file changes", CommandOptionType.NoValue);
            var optionPackages = app.Option("--packages <PACKAGE_DIR>", "Directory containing packages", CommandOptionType.SingleValue, c => 
            {

            });
            app.HelpOption("-?|-h|--help");
            app.VersionOption("--version", "1.0.0");

            try
            {
                app.Execute(args);
            }
            catch (Exception ex) when(ex.Message.StartsWith("TODO: Error: unrecognized option"))
            {
                return 1;
            }

            if (app.IsShowingInformation)
            {
                // If help option or version option was specified, exit immediately with 0 exit code
                return 0;
            }

            // further process
            return 1;
        }
    }
}