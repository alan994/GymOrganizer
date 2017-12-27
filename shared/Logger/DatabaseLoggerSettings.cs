using Microsoft.Extensions.Logging;
using System;

namespace Logger
{
    public class DatabaseLoggerSettings
    {
        public string TableName { get; set; }
        public string ConnectionString { get; set; }
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
    }
}
