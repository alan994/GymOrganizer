using Data.Db;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utils;

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
                return TokenHelper.ExtractTenantFromToken(this.User, this.db);
            }
        }

        public Guid UserId
        {
            get
            {
                return TokenHelper.ExtractUserFromToken(this.User, this.db);
            }
        }
    }
}
