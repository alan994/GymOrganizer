using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class TenantQuery
    {
        public static IQueryable<Tenant> GetAllTenants(this GymOrganizerContext db)
        {
            return db.Tenants.Where(x => x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<Tenant> GetAllActiveTenants(this GymOrganizerContext db)
        {
            return GetAllTenants(db).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<Tenant>GetTenantById(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllTenants(db).Where(x => x.Id == tenantId);
        }
    }
}
