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
    public class AddOfficeHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<AddOfficeHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public AddOfficeHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<AddOfficeHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.AddOffice);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                OfficeQueue officeQueue = JsonConvert.DeserializeObject<OfficeQueue>(data);
                OfficeLogic officeLogic = new OfficeLogic(this.db, result.AdditionalData, this.loggerFactory);
                var officeId = await officeLogic.AddOffice(officeQueue);

                result.AdditionalData.Add("officeId", officeId.ToString());
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
