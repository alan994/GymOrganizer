using BusinessLogic.Model;
using Helper.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using Logger;

namespace BusinessLogic.Handlers
{
    public class HandlerBase
    {
        private ILogger<HandlerBase> logger;

        public HandlerBase(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<HandlerBase>();
        }

        protected void HandleException(Exception exception, QueueResult queueResult, QueueBase queueBase)
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

            #region Helper
            int? intCode = null;
            if (code.HasValue)
            {
                intCode = (int)code.Value;
            }

            string tenantId = null;
            string userId = null;

            if(queueBase != null)
            {
                tenantId = queueBase.TenantId.ToString();
                userId= queueBase.UserPerformingAction.ToString();
            }
            #endregion

            this.logger.LogCustomError($"Failed to execute action", exception, intCode, tenantId, userId);
        }
    }
}
