using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.City
{
    public class DeleteCityHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<DeleteCityHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public DeleteCityHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<DeleteCityHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.DeleteCity);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            CityQueue cityQueue = null;
            try
            {
                cityQueue = JsonConvert.DeserializeObject<CityQueue>(data);
                CityLogic cityLogic = new CityLogic(this.db, result.AdditionalData, this.loggerFactory);
                await cityLogic.DeleteCity(cityQueue);

                result.AdditionalData.Add("cityId", cityQueue.Id.ToString());
                result.AdditionalData.Add("cityName", cityQueue.Name);

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, cityQueue);
            }
            return result;
        }
    }
}
