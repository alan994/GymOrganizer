using System;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Microsoft.Extensions.Logging;
using Helper.Configuration;
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Helper.Exceptions;

namespace BusinessLogic.Handlers.User
{
    public class DeleteUserHandler : HandlerBase, IHandler
    {
        private readonly AppSettings settings;
        private readonly ILogger<DeleteUserHandler> logger;
        private readonly GymOrganizerContext db;
        private readonly ILoggerFactory loggerFactory;

        public DeleteUserHandler(GymOrganizerContext db, ILoggerFactory loggerFactory, AppSettings settings) : base(loggerFactory)
        {
            this.settings = settings;
            this.logger = loggerFactory.CreateLogger<DeleteUserHandler>();
            this.db = db;
            this.loggerFactory = loggerFactory;
        }

        public async Task<QueueResult> Handle(string data)
        {
            QueueResult result = new QueueResult(Data.Enums.ProcessType.EditUser);

            if (string.IsNullOrEmpty(data))
            {
                result.ExceptionCode = ExceptionCode.MissingQueueData;
            }
            try
            {
                UserQueue userQueue = JsonConvert.DeserializeObject<UserQueue>(data);
                UserLogic userLogic = new UserLogic(this.db, result.AdditionalData, this.loggerFactory, this.settings);
                await userLogic.DeleteUser(userQueue);

                result.AdditionalData.Add("userId", userQueue.Id.Value.ToString());
                result.AdditionalData.Add("userFirstName", userQueue.FirstName);
                result.AdditionalData.Add("userLastName", userQueue.LastName);

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
