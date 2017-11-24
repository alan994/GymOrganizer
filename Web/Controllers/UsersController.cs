using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Db;
using Microsoft.Extensions.Logging;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/users")]
    public class UsersController : AuthController
    {
        private readonly ILogger<UsersController> logger;
        private readonly GymOrganizerContext db;
        public readonly UserService UserService;

        public UsersController(GymOrganizerContext db, ILogger<UsersController> logger, UserService userService) : base(db)
        {
            this.logger = logger;
            this.db = db;
            this.UserService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserVM> resultList = await this.UserService.GetAllUsers();
            return Ok(resultList);
        }
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            List<UserVM> resultList = await this.UserService.GetAllAcitveUsers();
            return Ok(resultList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            UserVM user = await this.UserService.GetUserById(id);
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserVM user)
        {
            await this.UserService.AddUser(user);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]UserVM user)
        {
            await this.UserService.EditUser(user);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.UserService.DeleteUser(id);
            return Ok();
        }

    }
}
