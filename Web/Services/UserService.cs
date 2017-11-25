using AutoMapper;
using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Queue;
using Web.ViewModels;

namespace Web.Services
{
    public class UserService : ServiceBase
    {
        private readonly GymOrganizerContext db;
        private readonly ILogger<UserService> logger;
        private readonly IQueueHandler queueHandler;

        public UserService(GymOrganizerContext db, ILogger<UserService> logger, IQueueHandler queueHandler)
        {
            this.db = db;
            this.logger = logger;
            this.queueHandler = queueHandler;
        }

        public async Task<List<UserVM>> GetAllUsers()
        {
            List<UserVM> resultList = await this.db.GetAllUsers(this.TenantId)
                .Select(x => Mapper.Map<UserVM>(x))
                .ToListAsync();

            await FillRoles(resultList);

            return resultList;
        }

        private async Task FillRoles(List<UserVM> users)
        {
            var userRoles = await this.db.UserRoles.ToListAsync();
            var applicationRoles = await this.db.Roles.ToListAsync();
            foreach (var user in users)
            {
                var roles = userRoles.Where(x => x.UserId == user.Id).ToList();
                foreach (var role in applicationRoles)
                {
                    if (role.NormalizedName == "ADMINISTRATOR" && roles.FirstOrDefault(x => x.RoleId == role.Id) != null)
                    {
                        user.IsAdmin = true;
                    }
                    if (role.NormalizedName == "COACH" && roles.FirstOrDefault(x => x.RoleId == role.Id) != null)
                    {
                        user.IsCoach = true;
                    }
                    if (role.NormalizedName == "GLOBAL_ADMIN" && roles.FirstOrDefault(x => x.RoleId == role.Id) != null)
                    {
                        user.IsGlobalAdmin = true;
                    }
                }
            }
        }

        public async Task<List<UserVM>> GetAllAcitveUsers()
        {
            var resultList = await this.db.GetAllActiveUsers(this.TenantId)
                .Select(x => Mapper.Map<UserVM>(x))
                .ToListAsync();

            await FillRoles(resultList);
            return resultList;
        }

        public async Task<UserVM> GetUserById(Guid id)
        {
            return await this.db.GetUserById(this.TenantId, id)
                .Select(x => Mapper.Map<UserVM>(x))
                .FirstOrDefaultAsync();
        }

        public async Task AddUser(UserVM user)
        {
            UserQueue userQueue = Mapper.Map<UserVM, UserQueue>(user, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(userQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddUser
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditUser(UserVM user)
        {
            UserQueue userQueue = Mapper.Map<UserVM, UserQueue>(user, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(userQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditUser
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task DeleteUser(Guid id)
        {
            UserQueue userQueue = new UserQueue()
            {
                Id = id,
                TenantId = this.TenantId,
                UserPerformingAction = this.UserId
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(userQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.DeleteUser
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }
    }
}
