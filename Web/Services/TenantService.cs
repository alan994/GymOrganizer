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
    public class TenantService : ServiceBase
    {
        private readonly GymOrganizerContext db;
        private readonly IQueueHandler queueHandler;
        private readonly ILogger<TenantService> logger;

        public TenantService(ILogger<TenantService> logger, GymOrganizerContext db, IQueueHandler queueHandler)
        {
            this.db = db;
            this.queueHandler = queueHandler;
            this.logger = logger;
        }

        public async Task<List<TenantVM>> GetAllTenants()
        {
            return await this.db.GetAllTenants()       
                .Select(x => Mapper.Map<TenantVM>(x))
                .ToListAsync();
        }

        public async Task<List<TenantVM>> GetAllActiveTenants()
        {
            return await this.db.GetAllActiveTenants()                
                .Select(x => Mapper.Map<TenantVM>(x))
                .ToListAsync();
        }

        public async Task<TenantVM> GetTenantById(Guid id)
        {
            return await this.db.GetTenantById(id)                
                .Select(x => Mapper.Map<TenantVM>(x))
                .FirstOrDefaultAsync();
        }

        public async Task AddTenant(TenantVM tenant)
        {
            TenantQueue officeQueue = Mapper.Map<TenantVM, TenantQueue>(tenant, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(officeQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddTenant
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditTenant(TenantVM tenant)
        {
            TenantQueue officeQueue = Mapper.Map<TenantVM, TenantQueue>(tenant, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(officeQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditTenant
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        //public async Task DeleteOffice(Guid id)
        //{
        //    TenantQueue tenantQueue = new TenantQueue()
        //    {
        //        Id = id,
        //        TenantId = this.TenantId,
        //        UserPerformingAction = this.UserId
        //    };

        //    ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
        //    {
        //        Data = JsonConvert.SerializeObject(tenantQueue),
        //        AddedById = this.UserId,
        //        TenantId = this.TenantId,
        //        Status = Data.Enums.ResultStatus.Waiting,
        //        Type = Data.Enums.ProcessType.D
        //    };
        //    await this.queueHandler.AddToQueue(processQueueHistory);
        //}

    }
}
