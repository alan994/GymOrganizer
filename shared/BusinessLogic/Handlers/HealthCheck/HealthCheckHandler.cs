using BusinessLogic.Model;
using Data.Db;
using Helper.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers.HealthCheck
{
    public class HealthCheckHandler : HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<HealthCheckHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public HealthCheckHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<HealthCheckHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.HealthCheck);
            
            try
            {              
                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                HandleException(ex, result, null);
            }
            return result;
        }
    }
}
