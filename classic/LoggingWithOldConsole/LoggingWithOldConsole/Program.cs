using System;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace LoggingWithOldConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new DiagnosticsLoggerProvider());
            loggerFactory.AddConsole();

            FooManager manager = new FooManager(loggerFactory);
            manager.Run();
        }
    }

    public class FooManager
    {
        private readonly ILogger _logger;

        public FooManager(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }

            _logger = loggerFactory.Create<FooManager>();
        }

        public void Run()
        {
            _logger.WriteVerbose("Writing verbose...");
            _logger.WriteInformation("Writing info...");
            _logger.WriteWarning("Writing warning...");
            _logger.WriteError("Writing error...");
            _logger.WriteCritical("Writing critical...");
        }
    }
}