using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.Tenant
{
    public class AddTenantHandler : HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<AddTenantHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public AddTenantHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<AddTenantHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.AddTenant);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                TenantQueue tenantQueue = JsonConvert.DeserializeObject<TenantQueue>(data);
                TenantLogic tenantLogic = new TenantLogic(this.db, result.AdditionalData, this.loggerFactory);
                var tenantId = await tenantLogic.AddTenant(tenantQueue);

                result.AdditionalData.Add("tenantId", tenantId.ToString());
                result.AdditionalData.Add("name", tenantQueue.Name);                

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result);
            }
            return result;
        }
    }
}
