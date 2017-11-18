using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class CountryQuery
    {
        public static IQueryable<Country> GetAllCountries(this GymOrganizerContext db, Guid tenantId)
        {
            return db.Countries.Where(x => x.TenantId == tenantId && x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<Country> GetAllActiveCountries(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllCountries(db, tenantId).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<Country> GetCountryById(this GymOrganizerContext db, Guid tenantId, Guid countryId)
        {
            return GetAllCountries(db, tenantId).Where(x => x.Id == countryId);
        }

    }
}
