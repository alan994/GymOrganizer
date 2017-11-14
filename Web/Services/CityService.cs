using AutoMapper;
using BusinessLogic.Model;
using Data.Db;
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
    public class CityService : ServiceBase
    {
        private readonly GymOrganizerContext db;
        private readonly IQueueHandler queueHandler;

        public CityService(GymOrganizerContext db, IQueueHandler queueHandler)
        {
            this.db = db;
            this.queueHandler = queueHandler;
        }

        public async Task<List<CityVM>> GetAllCitiesAsync()
        {
            return await this.db.Cities.Where(x => x.Status != Data.Enums.ExistanceStatus.Deleted && x.TenantId == this.TenantId)
                .Include(x => x.Country)
                .Select(x => Mapper.Map<CityVM>(x))
                .ToListAsync();

        }

        public async Task<CityVM> GetCityByIdAsync(Guid id)
        {
            return await this.db.Cities.Where(x => x.Status != Data.Enums.ExistanceStatus.Deleted && x.TenantId == this.TenantId && x.Id == id)
                .Include(x => x.Country)
                .Select(x => Mapper.Map<CityVM>(x))
                .FirstOrDefaultAsync();
        }

        public async Task AddCity(CityVM city)
        {
            CityQueue cityQueue = Mapper.Map<CityVM, CityQueue>(city, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(cityQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.AddCity
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task EditCity(CityVM city)
        {
            CityQueue cityQueue = Mapper.Map<CityVM, CityQueue>(city, options => {
                options.AfterMap((src, dest) => dest.UserPerformingAction = this.UserId);
                options.AfterMap((src, dest) => dest.TenantId = this.TenantId);
            });

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(cityQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.EditCity
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }

        public async Task DeleteCity(Guid id)
        {
            CityQueue cityQueue = new CityQueue()
            {
                Id = id
            };

            ProcessQueueHistory processQueueHistory = new ProcessQueueHistory()
            {
                Data = JsonConvert.SerializeObject(cityQueue),
                AddedById = this.UserId,
                TenantId = this.TenantId,
                Status = Data.Enums.ResultStatus.Waiting,
                Type = Data.Enums.ProcessType.DeleteCity
            };
            await this.queueHandler.AddToQueue(processQueueHistory);
        }
    }
}
