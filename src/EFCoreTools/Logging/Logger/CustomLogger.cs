using EFCoreTools.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EFCoreTools.Logging.Logger
{
    public class CustomLogger : ILogger
    {
        private readonly string _name;
        private readonly CustomLoggerConfiguration _config;

        private static readonly List<string> _enabledEventNameList = new List<string>() { "CommandExecuting", "CommandExecuted" };
        public CustomLogger(string name, CustomLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel, EventId eventId)
        {
            return (logLevel == LogLevel.Debug || logLevel == LogLevel.Information) && _enabledEventNameList.Any(eventId.Name.Contains);
        }

        private static ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel, eventId))
                return;

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var logMessage = formatter(state, exception);

               //Write your own log mechanism.
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (logLevel == LogLevel.Debug || logLevel == LogLevel.Information);
        }
    }
}
