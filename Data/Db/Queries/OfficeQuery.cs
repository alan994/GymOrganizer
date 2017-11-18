using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class OfficeQuery
    {
        public static IQueryable<Office> GetAllOffices(this GymOrganizerContext db, Guid tenantId)
        {
            return db.Offices.Where(x => x.TenantId == tenantId && x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<Office> GetAllActiveOffices(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllOffices(db, tenantId).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<Office> GetOfficeById(this GymOrganizerContext db, Guid tenantId, Guid officeId)
        {
            return GetAllOffices(db, tenantId).Where(x => x.Id == officeId);
        }

        public static IQueryable<Office> GetAllOfficesForCity(this GymOrganizerContext db, Guid tenantId, Guid cityId)
        {
            return GetAllOffices(db, tenantId).Where(x => x.CityId == cityId);
        }
    }
}
