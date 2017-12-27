using Data.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Utils
{
    public static class TokenHelper
    {
        public static Guid ExtractTenantFromToken(ClaimsPrincipal httpUser, GymOrganizerContext db)
        {
            if (httpUser.Identity.IsAuthenticated)
            {
                var userId = httpUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var userIdGuid = Guid.Parse(userId);
                    var user = db.Users.FirstOrDefault(x => x.Id == userIdGuid);
                    if (user != null)
                    {
                        return user.TenantId;
                    }
                }
            }
            return Guid.Empty;
        }

        public static Guid ExtractUserFromToken(ClaimsPrincipal httpUser, GymOrganizerContext db)
        {
            if (httpUser.Identity.IsAuthenticated)
            {
                var userId = httpUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    return Guid.Parse(userId);
                }
            }
            return Guid.Empty;
        }
    }
}
