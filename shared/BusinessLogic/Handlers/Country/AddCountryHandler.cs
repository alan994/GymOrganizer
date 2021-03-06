﻿using System;
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
    public class AddCountryHandler : HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<AddCountryHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public AddCountryHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<AddCountryHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.AddCountry);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }

            CountryQueue countryQueue = null;
            try
            {
                countryQueue = JsonConvert.DeserializeObject<CountryQueue>(data);
                CountryLogic countryLogic = new CountryLogic(this.db, result.AdditionalData, this.loggerFactory);
                var countryId = await countryLogic.AddCountry(countryQueue);

                result.AdditionalData.Add("countryId", countryId.ToString());
                result.AdditionalData.Add("countryName", countryQueue.Name);

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, countryQueue);
            }
            return result;
        }
    }
}

