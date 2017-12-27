using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    internal class DatabaseLogger : ILogger, IDisposable
    {
        private readonly string categoryName;
        private readonly DatabaseLoggerSettings settings;

        public DatabaseLogger(string categoryName, DatabaseLoggerSettings settings)
        {
            this.categoryName = categoryName;
            this.settings = settings;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            var isPerformaInfo = message.StartsWith($"_%%%Info prefix%%%_");

            if (logLevel < this.settings.MinimumLogLevel && !isPerformaInfo)
            {
                return;
            }

            if (isPerformaInfo)
            {
                message = message.Substring(message.IndexOf("_%%%Info prefix%%%_") + 19, message.Length - message.IndexOf("_%%%Info prefix%%%_") - 19);
            }
            string exceptionMsg = null;

            if (exception != null)
            {
                exceptionMsg = exception.StackTrace;
            }
            LoggerService service = new LoggerService(this.settings);

            string tenantId = null;
            string userId = null;
            int errorCode = int.MinValue;
            var realMessage = message;

            try
            {
                var hasTenantId = message.Contains("_&&&") && message.Contains("&&&_");
                var hasUserId = message.Contains("_$$$") && message.Contains("$$$_");
                var hasErrorCode = message.Contains("_###") && message.Contains("###_");

                tenantId = hasTenantId ? ExtrudeTenantId(message) : null;
                userId = hasUserId ? ExtrudeUserId(message) : null;
                errorCode = hasErrorCode ? ExtrudeErrorCode(message) : int.MinValue;

                if (!string.IsNullOrEmpty(tenantId))
                {
                    var tenantIdPrefix = $"_&&&{tenantId}&&&_";
                    message = message.Remove(message.IndexOf(tenantIdPrefix), tenantIdPrefix.Length);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    var userIdPrefix = $"_$$${userId}$$$_";
                    message = message.Remove(message.IndexOf(userIdPrefix), userIdPrefix.Length);
                }
                if (errorCode != int.MinValue)
                {
                    var errorCodePrefix = $"_###{errorCode}###_";
                    message = message.Remove(message.IndexOf(errorCodePrefix), errorCodePrefix.Length);
                }
                realMessage = message;
            }
            catch (Exception)
            {
                tenantId = null;
                userId = null;
                errorCode = int.MinValue;
            }


            if (exception != null)
            {
                realMessage += " MESSAGE: " + exception.Message;
            }

            service.Write(realMessage, GetLogLevelInt(logLevel), exceptionMsg, errorCode, this.categoryName, eventId.Id, tenantId, userId);
        }

        private int ExtrudeErrorCode(string message)
        {
            return int.Parse(message.Substring(message.IndexOf("_###") + 4, message.IndexOf("###_") - 4 - message.IndexOf("_###")));
        }

        private string ExtrudeTenantId(string message)
        {
            return message.Substring(message.IndexOf("_&&&") + 4, message.IndexOf("&&&_") - 4 - message.IndexOf("_&&&"));
        }
        private string ExtrudeUserId(string message)
        {
            return message.Substring(message.IndexOf("_$$$") + 4, message.IndexOf("$$$_") - 4 - message.IndexOf("_$$$"));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        private static int GetLogLevelInt(LogLevel logLevel)
        {
            return (int)logLevel;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return false;
                case LogLevel.Trace:
                    return false;
                case LogLevel.None:
                    return false;
                default:
                    return true;
            }
        }


        public void Dispose()
        {

        }
    }
}
