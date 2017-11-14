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
    public class AddCityHandler : HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<AddCityHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public AddCityHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<AddCityHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }
        
        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult();

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {                
                CityQueue cityQueue = JsonConvert.DeserializeObject<CityQueue>(data);
                CityLogic cityLogic = new CityLogic(this.db, result.AdditionalData, this.loggerFactory);
                await cityLogic.AddCity(cityQueue);
                result.Status = Status.Success;
            }
            catch(Exception ex)
            {
                HandleException(ex, result);
            }
            return result;
        }
    }
}
