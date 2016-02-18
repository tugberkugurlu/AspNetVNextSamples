using System;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Framework.Runtime;
using System.Runtime.Versioning;
using System.Globalization;

namespace Logging.ConsoleSample
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnv;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public Program(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddConsole();

            _logger = _loggerFactory.Create<Program>();
        }

        public void Main(params string[] args)
        {
            _logger.Write(
                TraceType.Information,
                0,
                "appEnv.ApplicationName: {0}", null,
                (state, ex) => string.Format((string)state, _appEnv.ApplicationName));

            _logger.Write(
                TraceType.Information,
                0,
                "appEnv.Version: {0}", null,
                (state, ex) => string.Format((string)state, _appEnv.Version));

            _logger.Write(
                TraceType.Information,
                0,
                "appEnv.ApplicationBasePath: {0}", null,
                (state, ex) => string.Format((string)state, _appEnv.ApplicationBasePath));

            _logger.Write(
                TraceType.Information,
                0,
                "appEnv.Configuration: {0}", null,
                (state, ex) => string.Format((string)state, _appEnv.Configuration));

            _logger.Write(
                TraceType.Information,
                0,
                "appEnv.RuntimeFramework: FullName={0}, Identifier={1}, Profile={2}, Version={3}", null,
                (state, ex) => string.Format((string)state, _appEnv.RuntimeFramework.FullName, _appEnv.RuntimeFramework.Identifier, _appEnv.RuntimeFramework.Profile, _appEnv.RuntimeFramework.Version));

            _logger.WriteSpecial(
                "appEnv.RuntimeFramework: FullName={0}, Identifier={1}, Profile={2}, Version={3}",
                _appEnv.RuntimeFramework.FullName,
                _appEnv.RuntimeFramework.Identifier,
                _appEnv.RuntimeFramework.Profile,
                _appEnv.RuntimeFramework.Version.ToString());
        }
    }

    public static class LoggerExtensions
    {
        private static readonly Func<object, Exception, string> TheMessage = (msgWithArgs, error) =>
            string.Format(CultureInfo.CurrentCulture, ((Tuple<string, string[]>)msgWithArgs).Item1, ((Tuple<string, string[]>)msgWithArgs).Item2);

        public static void WriteSpecial(this ILogger logger, string message, params string[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.Write(TraceType.Warning, 0, Tuple.Create(message, args), null, TheMessage);
        }
    }
}