using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Data.Db;
using Data.Model;
using HealthAnalyzer.ViewModels;
using Helper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Utils.Queue;

namespace HealthAnalyzer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GymOrganizerContext db;
        public List<LogVM> logs;
        private readonly AppSettings appSettings;
        private readonly IQueueHandler queueHandler;

        public List<WorkerVM> WorkerTasks { get; set; }


        public IndexModel(GymOrganizerContext db, AppSettings appSettings, IQueueHandler queueHandler)
        {
            this.db = db;
            this.appSettings = appSettings;
            this.queueHandler = queueHandler;
        }

        public async Task OnGet()
        {
            await SendDummyTask();
            await GetLatestLogs(null);
            await GetHealthCheckData();
        }

        private async Task SendDummyTask()
        {
            HealthCheckQueue healthCheckQueue = new HealthCheckQueue()
            {
                UserPerformingAction = Guid.Empty,
                TenantId = Guid.Empty
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(healthCheckQueue),
                AddedById = null,
                TenantId = null,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.HealthCheck
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        private async Task GetHealthCheckData()
        {
            this.WorkerTasks = await this.db.ProcessQueuesHistory
                .Where(x => x.Type == Data.Enums.ProcessType.HealthCheck)
                .OrderByDescending(x => x.AddedToQueue)
                .Take(200)
                .Select(x => new WorkerVM()
                {
                    ProcessTime = x.FinishedAt,
                    SentTime = x.AddedToQueue,
                    Status = (int)x.Status,
                    Id = x.Id
                })
                .ToListAsync();
        }

        private async Task GetLatestLogs(Guid? tenantId)
        {
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(this.appSettings.Data.Logs.ConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "GetLogs";

                    SqlParameter parameter = command.CreateParameter();

                    parameter.ParameterName = "@tenantId";
                    if (tenantId.HasValue)
                    {
                        parameter.Direction = System.Data.ParameterDirection.Input;
                        parameter.Value = tenantId.Value.ToString();
                    }
                    else
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);

                    reader = await command.ExecuteReaderAsync();

                    this.logs = new List<LogVM>();
                    if (reader != null && reader.HasRows)
                    {
                        int idOrder = reader.GetOrdinal("ID");
                        int errorCodeOrder = reader.GetOrdinal("ErrorCode");
                        int msgOrder = reader.GetOrdinal("Message");
                        int levelOrder = reader.GetOrdinal("Level");
                        int dateOrder = reader.GetOrdinal("TimeStamp");
                        int userIdOrder = reader.GetOrdinal("UserId");                        
                        int exceptionOrder = reader.GetOrdinal("Exception");
                        int tenantIdOrder = reader.GetOrdinal("Tenant");

                        while (reader.Read())
                        {
                            LogVM log = new LogVM();

                            if (!reader.IsDBNull(errorCodeOrder))
                            {
                                log.ErrorCode = reader.GetInt32(errorCodeOrder);
                            }

                            log.Id = reader.GetInt32(idOrder);
                            log.UserId = reader.IsDBNull(userIdOrder) ? null : reader.GetString(userIdOrder);
                            log.TenantId = reader.IsDBNull(tenantIdOrder) ? null : reader.GetString(tenantIdOrder);
                            log.Message = reader.IsDBNull(msgOrder) ? null : reader.GetString(msgOrder);
                            log.ExceptionMessage = reader.IsDBNull(exceptionOrder) ? null : reader.GetString(exceptionOrder);
                            log.Level = reader.GetInt32(levelOrder);
                            log.Date = reader.GetDateTime(dateOrder);

                            this.logs.Add(log);
                        }
                    }
                }
            }
        }
    }
}
