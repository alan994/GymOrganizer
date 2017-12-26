using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Helper.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Logger;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class CountryLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<CountryLogic> logger;

        public CountryLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<CountryLogic>();
        }

        public async Task<Guid> AddCountry(CountryQueue countryQueue)
        {
            await CheckAddEdit(countryQueue);

            Country country = new Country()
            {
                Name = countryQueue.Name,
                Iso2Code = countryQueue.Iso2Code,
                Iso3Code = countryQueue.Iso3Code,
                NumericCode = countryQueue.NumericCode,                
                Status = countryQueue.Status,
                TenantId = countryQueue.TenantId
            };

            this.db.Countries.Add(country);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"County '{country.Name}' with id '{country.Id}' has successfully created", countryQueue.TenantId.ToString(), countryQueue.UserPerformingAction.ToString());
            return country.Id;
        }

        public async Task EditCountry(CountryQueue countryQueue)
        {
            await CheckAddEdit(countryQueue);

            Country country = await this.db.GetCountryById(countryQueue.TenantId, countryQueue.Id.Value).FirstOrDefaultAsync();
            country.Name = countryQueue.Name;
            country.Iso3Code = countryQueue.Iso3Code;
            country.Iso2Code = countryQueue.Iso2Code;
            country.NumericCode = countryQueue.NumericCode;
            country.Status = countryQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogCustomInformation($"Country '{country.Name}' with id '{country.Id}' has successfully updated", countryQueue.TenantId.ToString(), countryQueue.UserPerformingAction.ToString());

        }

        public async Task DeleteCountry(CountryQueue countryQueue)
        {
            await CheckDelete(countryQueue);

            Country country = await this.db.GetCountryById(countryQueue.TenantId, countryQueue.Id.Value).FirstOrDefaultAsync();

            this.db.Countries.Remove(country);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Country '{country.Name}' with id '{country.Id}' has successfully deleted", countryQueue.TenantId.ToString(), countryQueue.UserPerformingAction.ToString());
        }

        private async Task CheckAddEdit(CountryQueue countryQueue)
        {
            await CheckForSameName(countryQueue.Name.Trim(), countryQueue.Id, countryQueue.TenantId);
        }

        private async Task CheckForSameName(string name, Guid? countryId, Guid tenantId)
        {
            Country countryWithTheSameName = null;
            if (countryId.HasValue)
            {
                countryWithTheSameName = await this.db
                    .GetAllCountries(tenantId)
                    .Where(x => x.Name == name && x.Id != countryId.Value)
                    .FirstOrDefaultAsync();
            }
            else
            {
                countryWithTheSameName = await this.db
                    .GetAllCountries(tenantId)
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();
            }

            if (countryWithTheSameName != null)
            {
                throw new NameAlreadyExists($"Country ({countryWithTheSameName.Id}) with the same name ({name}) already exists");
            }
        }


        private async Task CheckDelete(CountryQueue countryQueue)
        {
            var relatedCity = await this.db.GetAllCitiesForCountry(countryQueue.TenantId, countryQueue.Id.Value).FirstOrDefaultAsync();
            if (relatedCity != null)
            {
                throw new CountryHasRelatedCity($"Country ({countryQueue.Id}) has related city ({relatedCity.Id})", relatedCity.Id, relatedCity.Name);
            }
        }
    }
}
