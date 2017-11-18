using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Db.Queries
{
    public static class TermQuery
    {
        public static IQueryable<Term> GetAllTerms(this GymOrganizerContext db, Guid tenantId)
        {
            return db.Terms.Where(x => x.TenantId == tenantId && x.Status != Enums.ExistenceStatus.Deleted);
        }

        public static IQueryable<Term> GetAllActiveTerms(this GymOrganizerContext db, Guid tenantId)
        {
            return GetAllTerms(db, tenantId).Where(x => x.Status == Enums.ExistenceStatus.Active);
        }

        public static IQueryable<Term> GetTermById(this GymOrganizerContext db, Guid tenantId, Guid termId)
        {
            return GetAllTerms(db, tenantId).Where(x => x.Id == termId);
        }

        public static IQueryable<Term> GetAllTermsForOffice(this GymOrganizerContext db, Guid tenantId, Guid officeId)
        {
            return GetAllTerms(db, tenantId).Where(x => x.OfficeId == officeId);
        }
    }
}
