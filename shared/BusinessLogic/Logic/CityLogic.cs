using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Helper.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class CityLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<CityLogic> logger;

        public CityLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<CityLogic>();
        }

        public async Task<Guid> AddCity(CityQueue cityQueue)
        {
            await CheckAddEdit(cityQueue);
            City city = new City()
            {
                Name = cityQueue.Name,
                PostalCode = cityQueue.PostalCode,
                CountryId = cityQueue.CountryId,
                Status = cityQueue.Status,
                TenantId = cityQueue.TenantId
            };

            this.db.Cities.Add(city);
            await this.db.SaveChangesAsync();
            
            this.logger.LogInformation($"City '{city.Name}' with id '{city.Id}' has successfully created");
            return city.Id;
        }

        public async Task EditCity(CityQueue cityQueue)
        {
            await CheckAddEdit(cityQueue);

            City city = await this.db.GetCityById(cityQueue.TenantId, cityQueue.Id.Value).FirstOrDefaultAsync();
            city.Name = cityQueue.Name;
            city.PostalCode = cityQueue.PostalCode;
            city.CountryId = cityQueue.CountryId;
            city.Status = cityQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogInformation($"City '{city.Name}' with id '{city.Id}' has successfully updated");

        }

        public async Task DeleteCity(CityQueue cityQueue)
        {
            await CheckDelete(cityQueue);

            City city = await this.db.GetCityById(cityQueue.TenantId, cityQueue.Id.Value).FirstOrDefaultAsync();

            this.db.Cities.Remove(city);
            await this.db.SaveChangesAsync();

            this.logger.LogInformation($"City '{city.Name}' with id '{city.Id}' has successfully deleted");
        }

        private async Task CheckAddEdit(CityQueue cityQueue)
        {
            await CheckForSameName(cityQueue.Name.Trim(), cityQueue.Id, cityQueue.TenantId);
        }

        private async Task CheckForSameName(string name, Guid? cityId, Guid tenantId)
        {
            City cityWithTheSameName = null;
            if (cityId.HasValue)
            {
                cityWithTheSameName = await this.db
                    .GetAllCities(tenantId)
                    .Where(x => x.Name == name && x.Id != cityId.Value)
                    .FirstOrDefaultAsync();
            }
            else
            {
                cityWithTheSameName = await this.db
                    .GetAllCities(tenantId)
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();
            }

            if (cityWithTheSameName != null)
            {
                throw new NameAlreadyExists($"City ({cityWithTheSameName.Id}) with the same name ({name}) already exists");
            }
        }

        private async Task CheckDelete(CityQueue cityQueue)
        {
            var relatedOffice = await this.db.GetAllOfficesForCity(cityQueue.TenantId, cityQueue.Id.Value).FirstOrDefaultAsync();
            if (relatedOffice != null)
            {
                throw new CityHasRelatedOffice($"City ({cityQueue.Id}) has related office ({relatedOffice.Id})", relatedOffice.Id, relatedOffice.Name);
            }
        }
    }
}
