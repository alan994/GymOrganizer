using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private DatabaseLoggerSettings settings;

        public DatabaseLoggerProvider(DatabaseLoggerSettings settings)
        {
            this.settings = settings;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(categoryName, this.settings);
        }
    }
}
