using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public static class CustomLoggerExtensions
    {
        public static void LogCustomInformation(this ILogger logger, string message, string tenantId = null, string userId = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            var userMsg = string.Empty;
            var tenantMsg = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userMsg = $"_$$${userId}$$$_";

            }
            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantMsg = $"_&&&{tenantId}&&&_";
            }
            var infoPrefix = $"_%%%Info prefix%%%_";

            logger.LogInformation($"{infoPrefix}{message}{tenantMsg}{userMsg}");
        }

        public static void LogCustomWarning(this ILogger logger, string message, string tenantId = null, string userId = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            var userMsg = string.Empty;
            var tenantMsg = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userMsg = $"_$$${userId}$$$_";

            }
            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantMsg = $"_&&&{tenantId}&&&_";
            }
            logger.LogWarning($"{message}{tenantMsg}{userMsg}");
        }

        public static void LogCustomError(this ILogger logger, string message, Exception e, int? errorCode, string tenantId = null, string userId = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            var userMsg = string.Empty;
            var tenantMsg = string.Empty;
            var errorCodeMsg = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userMsg = $"_$$${userId}$$$_";

            }
            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantMsg = $"_&&&{tenantId}&&&_";
            }

            if (errorCode.HasValue)
            {
                errorCodeMsg = $"_###{errorCode.ToString()}###_";
            }

            logger.LogError(new EventId(), e, $"{message}{tenantMsg}{userMsg}{errorCodeMsg}");
        }
    }
}
