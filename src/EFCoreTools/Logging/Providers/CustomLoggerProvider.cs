using EFCoreTools.Logging.Configuration;
using EFCoreTools.Logging.Logger;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTools.Logging.Providers
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, CustomLogger> _loggers = new ConcurrentDictionary<string, CustomLogger>();
        public CustomLoggerProvider(CustomLoggerConfiguration config)
        {
            _config = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new CustomLogger(name, _config));
        }
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
