using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.Office
{
    public class DeleteOfficeHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<DeleteOfficeHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public DeleteOfficeHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<DeleteOfficeHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.DeleteOffice);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                OfficeQueue officeQueue = JsonConvert.DeserializeObject<OfficeQueue>(data);
                OfficeLogic officeLogic = new OfficeLogic(this.db, result.AdditionalData, this.loggerFactory);
                await officeLogic.DeleteOffice(officeQueue);

                result.AdditionalData.Add("officeId", officeQueue.Id.ToString());
                result.AdditionalData.Add("officeName", officeQueue.Name);

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
