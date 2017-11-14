using Data.Db;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly GymOrganizerContext db;

        public AuthController(GymOrganizerContext db)
        {
            this.db = db;
        }
        public Guid TenantId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var userId = this.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        var userIdGuid = Guid.Parse(userId);
                        var user = this.db.Users.FirstOrDefault(x => x.Id == userIdGuid);
                        if (user != null)
                        {
                            return user.TenantId;
                        }
                    }
                }
                return Guid.Empty;
            }
        }

        public Guid UserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var userId = this.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        return Guid.Parse(userId);
                    }
                }
                return Guid.Empty;
            }
        }


    }
}
