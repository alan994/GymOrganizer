using System;
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
    public class EditTermHandler: HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<EditTermHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public EditTermHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<EditTermHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.EditTerm);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                TermQueue termQueue = JsonConvert.DeserializeObject<TermQueue>(data);
                TermLogic termLogic = new TermLogic(this.db, result.AdditionalData, this.loggerFactory);
                await termLogic.EditTerm(termQueue);

                result.AdditionalData.Add("termId", termQueue.Id.ToString());
                result.AdditionalData.Add("termStart", termQueue.Start.ToString());
                result.AdditionalData.Add("termEnd", termQueue.End.ToString());

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
