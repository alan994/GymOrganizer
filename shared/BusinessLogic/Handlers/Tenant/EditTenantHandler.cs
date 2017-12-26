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
    public class EditTenantHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<EditTenantHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public EditTenantHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<EditTenantHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.EditTenant);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            TenantQueue tenantQueue = null;
            try
            {
                tenantQueue = JsonConvert.DeserializeObject<TenantQueue>(data);
                TenantLogic tenantLogic = new TenantLogic(this.db, result.AdditionalData, this.loggerFactory);
                await tenantLogic.EditTenant(tenantQueue);

                result.AdditionalData.Add("tenantId", tenantQueue.Id.ToString());
                result.AdditionalData.Add("name", tenantQueue.Name);

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, tenantQueue);
            }
            return result;
        }
    }
}
