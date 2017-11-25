using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;
using Data.Db;
using Data.Db.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Web.Controllers
{
    [Route("api/account")]
    [Authorize]
    public class AccountController : AuthController
    {
        private readonly ILogger<AccountController> logger;
        private readonly GymOrganizerContext db;

        public AccountController(GymOrganizerContext db, ILogger<AccountController> logger) : base(db)
        {
            this.logger = logger;
            this.db = db;
        }
        public async Task<IActionResult> GetAccountInfo()
        {
            var user = await this.db.GetUserById(this.TenantId, this.UserId).FirstOrDefaultAsync();
            var tenant = await this.db.GetTenantById(this.TenantId).FirstOrDefaultAsync();

            if (user == null || user.Status == Data.Enums.ExistenceStatus.Deleted || tenant == null || tenant.Status == Data.Enums.ExistenceStatus.Deleted)
            {
                throw new Exception($"Unauthorized user tries to login. UserId: {this.UserId}, TenantId: {this.TenantId}");
            }

            var userRoles = await this.db.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
            var applicationRoles = await this.db.Roles.ToListAsync();

            var isAdmin = false;
            var isCoach = false;
            var isGlobalAdmin = false;

            foreach (var role in applicationRoles)
            {
                foreach (var userRole in userRoles)
                {
                    if (role.NormalizedName == "ADMINISTRATOR" && userRole.RoleId == role.Id)
                    {
                        isAdmin = true;
                    }
                    if (role.NormalizedName == "COACH" && userRole.RoleId == role.Id)
                    {
                        isCoach = true;
                    }
                    if (role.NormalizedName == "GLOBAL_ADMIN" && userRole.RoleId == role.Id)
                    {
                        isGlobalAdmin = true;
                    }
                }
            }

            //TODO: implement features
            var vm = new AccountVM()
            {
                User = Mapper.Map<UserVM>(user),
                Tenant = Mapper.Map<TenantVM>(tenant)
            };

            vm.User.IsAdmin = isAdmin;
            vm.User.IsCoach = isCoach;
            vm.User.IsGlobalAdmin = isGlobalAdmin;

            return Json(vm);
        }
    }
}
