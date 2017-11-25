using Data.Enums;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/users")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> AddUser([FromBody] UserQueue userQueue)
        {
            User user = new User()
            {
                FirstName = userQueue.FirstName,
                LastName = userQueue.LastName,
                UserName = userQueue.Email,
                Email = userQueue.Email,
                TenantId = userQueue.TenantId,
                Owed = userQueue.Owed,
                Claimed = userQueue.Claimed,
                Status = ExistenceStatus.Active                
            };


            var result = await this.userManager.CreateAsync(user, userQueue.TempPassword);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, "Member");
                if (userQueue.IsAdmin)
                {
                    await this.userManager.AddToRoleAsync(user, "Administrator");
                }

                if (userQueue.IsCoach)
                {
                    await this.userManager.AddToRoleAsync(user, "Coach");
                }
                if (userQueue.IsGlobalAdmin)
                {
                    await this.userManager.AddToRoleAsync(user, "Global_admin");
                }
                return Ok(user.Id);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }

    public class UserQueue
    {
        public Guid? Id { get; set; }
        public decimal Owed { get; set; }
        public decimal Claimed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCoach { get; set; }
        public string TempPassword { get; set; }
        public Guid TenantId { get; set; }
        public ExistenceStatus Status { get; set; }
        public bool IsGlobalAdmin { get; set; }
    }
}
