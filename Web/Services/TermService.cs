using AutoMapper;
using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Queue;
using Web.ViewModels;

namespace Web.Services
{
    public class TermService : ServiceBase
    {
        private readonly GymOrganizerContext db;
        private readonly IQueueHandler queueHandler;

        public TermService(GymOrganizerContext db, IQueueHandler queueHandler)
        {
            this.db = db;
            this.queueHandler = queueHandler;
        }

        public async Task<List<TermVM>> GetAllTerms()
        {
            return await this.db.GetAllTerms(this.TenantId)
                .Include(x => x.Office)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.Coach)
                .Select(x => Mapper.Map<TermVM>(x))
                .ToListAsync();
        }

        public async Task<List<TermVM>> GetAllActiveTerms()
        {
            return await this.db.GetAllActiveTerms(this.TenantId)
                .Include(x => x.Office)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.Coach)
                .Select(x => Mapper.Map<TermVM>(x))
                .ToListAsync();
        }

        public async Task<TermVM> GetTermById(Guid id)
        {
            return await this.db.GetTermById(this.TenantId, id)
                .Include(x => x.Office)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.Coach)
                .Select(x => Mapper.Map<TermVM>(x))
                .FirstOrDefaultAsync();
        }

        public async Task AddTerm(TermVM term)
        {
            TermQueue termQueue = Mapper.Map<TermVM, TermQueue>(term, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(termQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddTerm
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditTerm(TermVM term)
        {
            TermQueue termQueue = Mapper.Map<TermVM, TermQueue>(term, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(termQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditTerm
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task DeleteTerm(Guid id)
        {
            TermQueue termQueue = new TermQueue()
            {
                Id = id,
                TenantId = this.TenantId,
                UserPerformingAction = this.UserId
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(termQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.DeleteTerm
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }
    }
}
