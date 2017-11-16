using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.Country
{
    public class DeleteCountryHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<DeleteCountryHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public DeleteCountryHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<DeleteCountryHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.DeleteCountry);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                CountryQueue countryQueue = JsonConvert.DeserializeObject<CountryQueue>(data);
                CountryLogic countryLogic = new CountryLogic(this.db, result.AdditionalData, this.loggerFactory);
                await countryLogic.DeleteCountry(countryQueue);

                result.AdditionalData.Add("countryId", countryQueue.Id.ToString());
                result.AdditionalData.Add("countryName", countryQueue.Name);

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
