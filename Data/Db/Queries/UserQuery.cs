using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class UserQuery
    {
        public static IQueryable<User> GetAllUsers(this GymOrganizerContext db, Guid tenantId)
        {
            return db.Users.Where(x => x.TenantId == tenantId && x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<User> GetAllActiveUsers(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllUsers(db, tenantId).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<User> GetUserById(this GymOrganizerContext db, Guid tenantId, Guid userId)
        {
            return GetAllUsers(db, tenantId).Where(x => x.Id == userId);
        }        
    }
}
