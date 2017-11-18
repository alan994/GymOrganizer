using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class CityQuery
    {
        public static IQueryable<City> GetAllCities(this GymOrganizerContext db, Guid tenantId)
        {
            return db.Cities.Where(x => x.TenantId == tenantId && x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<City> GetAllActiveCities(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllCities(db, tenantId).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<City> GetCityById(this GymOrganizerContext db, Guid tenantId, Guid cityId)
        {
            return GetAllCities(db, tenantId).Where(x => x.Id == cityId);
        }

        public static IQueryable<City> GetAllCitiesForCountry(this GymOrganizerContext db, Guid tenantId, Guid countryId)
        {
            return GetAllCities(db, tenantId).Where(x => x.CountryId == countryId);
        }
    }
}
