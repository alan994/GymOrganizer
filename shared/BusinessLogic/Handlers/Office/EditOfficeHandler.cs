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
    public class EditOfficeHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<EditOfficeHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public EditOfficeHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<EditOfficeHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.EditOffice);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            OfficeQueue officeQueue = null;
            try
            {
                officeQueue = JsonConvert.DeserializeObject<OfficeQueue>(data);
                OfficeLogic officeLogic = new OfficeLogic(this.db, result.AdditionalData, this.loggerFactory);
                await officeLogic.EditOffice(officeQueue);

                result.AdditionalData.Add("officeId", officeQueue.Id.ToString());
                result.AdditionalData.Add("officeName", officeQueue.Name);

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, officeQueue);
            }
            return result;
        }
    }
}
