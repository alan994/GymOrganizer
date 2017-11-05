using AutoMapper;
using BusinessLogic.Model;
using Data.Db;
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
    public class OfficeService : ServiceBase
    {
        private readonly ILogger<OfficeService> logger;
        private readonly GymOrganizerContext db;
        private readonly IQueueHandler queueHandler;

        public OfficeService(ILogger<OfficeService> logger, GymOrganizerContext db, IQueueHandler queueHandler)
        {
            this.logger = logger;
            this.db = db;
            this.queueHandler = queueHandler;
        }

        public async Task<List<OfficeVM>> GetAllOfficessAsync()
        {
            return await this.db.Offices.Where(x => x.TenantId == this.TenantId)
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .Select(x => Mapper.Map<OfficeVM>(x))
                .ToListAsync();
        }

        public async Task<OfficeVM> GetOfficeByIdAsync(Guid id)
        {
            return await this.db.Offices.Where(x => x.TenantId == this.TenantId && x.Id == id)
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .Select(x => Mapper.Map<OfficeVM>(x))
                .FirstOrDefaultAsync();
        }

        public async Task AddOffice(OfficeVM office)
        {
            OfficeQueue officeQueue = Mapper.Map<OfficeVM, OfficeQueue>(office, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(officeQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddOffice
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditOffice(OfficeVM office)
        {
            OfficeQueue officeQueue = Mapper.Map<OfficeVM, OfficeQueue>(office, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(officeQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditOffice
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task DeleteOffice(Guid id)
        {
            OfficeQueue officeQueue = new OfficeQueue()
            {
                Id = id
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(officeQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.DeleteOffice
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }
    }
}
