using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class Logger
    {
        private static Logger instance;
        private LoggerService service;
        private DatabaseLoggerSettings settings;
        private Logger()
        {

        }


        public void ConfigureLogger(DatabaseLoggerSettings settings)
        {
            this.settings = settings;
            this.service = new LoggerService(settings);
        }

        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        private bool IsAllowedToLog(LogLevel logLevel)
        {
            return (int)this.settings.MinimumLogLevel <= (int)logLevel;
        }

        public void LogDebug(string message, string source, string tenantId = null, string userId = null)
        {
            if (this.service != null && IsAllowedToLog(LogLevel.Debug))
            {
                this.service.Write(message, (int)LogLevel.Debug, null, int.MinValue, source, 1, tenantId, userId);
                return;
            }
            //throw new LoggerNotConfiguredException();
        }

        public void LogInformation(string message, string source, string tenantId = null, string userId = null)
        {
            if (this.service != null)
            {
                this.service.Write(message, (int)LogLevel.Information, null, int.MinValue, source, 1, tenantId, userId);
                return;
            }
            //throw new LoggerNotConfiguredException();
        }

        public void LogWarning(string message, string source, string tenantId = null, string userId = null)
        {
            if (this.service != null)
            {
                this.service.Write(message, (int)LogLevel.Warning, null, int.MinValue, source, 1, tenantId, userId);
                return;
            }
            //throw new LoggerNotConfiguredException();
        }

        public void LogError(string message, Exception e, int? errorCode, string source, string tenantId = null, string userId = null)
        {
            int errorCodeMsg = errorCode.HasValue ? errorCode.Value : int.MinValue;

            if (this.service != null)
            {
                if (e != null)
                {
                    this.service.Write(message + $" MESSAGE: {e.Message}", (int)LogLevel.Error, e.StackTrace, errorCodeMsg, source, 1, tenantId, userId);
                }
                else
                {
                    this.service.Write(message, (int)LogLevel.Error, null, errorCodeMsg, source, 1, tenantId, userId);
                }
                return;
            }
            //throw new LoggerNotConfiguredException();
        }
        public void LogCriticalError(string message, Exception e, int? errorCode, string source, string tenantId = null, string userId = null)
        {
            int errorCodeMsg = errorCode.HasValue ? errorCode.Value : int.MinValue;

            if (this.service != null)
            {
                if (e != null)
                {
                    this.service.Write(message + $" MESSAGE: {e.Message}", (int)LogLevel.Critical, e.StackTrace, errorCodeMsg, source, 1, tenantId, userId);
                }
                else
                {
                    this.service.Write(message, (int)LogLevel.Critical, null, errorCodeMsg, source, 1, tenantId, userId);
                }
                return;
            }
            //throw new LoggerNotConfiguredException();
        }
    }
}
