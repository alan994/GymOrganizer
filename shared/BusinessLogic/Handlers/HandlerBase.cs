using BusinessLogic.Model;
using Helper.Exceptions;
using Microsoft.Extensions.Logging;
using System;

namespace BusinessLogic.Handlers
{
    public class HandlerBase
    {
        private ILogger<HandlerBase> logger;

        public HandlerBase(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<HandlerBase>();
        }

        protected void HandleException(Exception exception, QueueResult queueResult)
        {
            ExceptionCode? code = null;
            if (exception is BusinessException)
            {
                code = (exception as BusinessException).Code;
                queueResult.ExceptionCode = code;

                //Concatenate additionalData
                foreach (var item in (exception as BusinessException).AdditionalData)
                {
                    queueResult.AdditionalData.Add(item.Key, item.Value);
                }
            }
            else
            {
                queueResult.ExceptionCode = ExceptionCode.Error;
            }
            queueResult.Status = Status.Fail;

            //TODO: log exception, code, tenantId, userId
            this.logger.LogError($"Failed to execute action");
        }
    }
}
