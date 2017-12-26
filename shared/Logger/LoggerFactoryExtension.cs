using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public static class LoggerFactoryExtension
    {
        public static ILoggerFactory AddDatabase(this ILoggerFactory factory, DatabaseLoggerSettings settings)
        {
            factory.AddProvider(new DatabaseLoggerProvider(settings));
            return factory;
        }
    }
}
