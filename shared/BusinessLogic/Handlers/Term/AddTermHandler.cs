﻿using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.Term
{
    public class AddTermHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<AddTermHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public AddTermHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<AddTermHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.AddTerm);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            TermQueue termQueue = null;
            try
            {
                termQueue = JsonConvert.DeserializeObject<TermQueue>(data);
                TermLogic termLogic = new TermLogic(this.db, result.AdditionalData, this.loggerFactory);
                var termId = await termLogic.AddTerm(termQueue);

                result.AdditionalData.Add("termId", termId.ToString());
                result.AdditionalData.Add("termStart", termQueue.Start.ToString());
                result.AdditionalData.Add("termEnd", termQueue.End.ToString());

                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, termQueue);
            }
            return result;
        }
    }
}
