using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTools.Logging.Configuration
{
    public class CustomLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; }
        public int EventId { get; set; } = 0;
    }
}
