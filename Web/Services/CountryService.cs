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
    public class CountryService : ServiceBase
    {
        private readonly GymOrganizerContext db;
        private readonly IQueueHandler queueHandler;

        public CountryService(GymOrganizerContext db, IQueueHandler queueHandler)
        {
            this.db = db;
            this.queueHandler = queueHandler;
        }

        public async Task<List<CountryVM>> GetAllCountries()
        {
            return await this.db.GetAllCountries(this.TenantId).Select(x => Mapper.Map<CountryVM>(x)).ToListAsync();
        }

        public async Task<List<CountryVM>> GetAllActiveCountries()
        {
            return await this.db.GetAllActiveCountries(this.TenantId).Select(x => Mapper.Map<CountryVM>(x)).ToListAsync();
        }
        public async Task<CountryVM> GetCountryById(Guid id)
        {
            return await this.db.GetCountryById(this.TenantId, id).Select(x => Mapper.Map<CountryVM>(x)).FirstOrDefaultAsync();   
        }

        public async Task AddCountry(CountryVM country)
        {
            CountryQueue countryQueue = Mapper.Map<CountryVM, CountryQueue>(country, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(countryQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddCountry
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditCountry(CountryVM country)
        {
            CountryQueue countryQueue = Mapper.Map<CountryVM, CountryQueue>(country, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(countryQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditCountry
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task DeleteCountry(Guid id)
        {
            CountryQueue countryQueue = new CountryQueue()
            {
                Id = id,
                TenantId = this.TenantId,
                UserPerformingAction = this.UserId
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(countryQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.DeleteCountry
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

    }
}
