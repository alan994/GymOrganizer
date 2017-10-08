using Data.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web
{
    public class TestController : ControllerBase
    {
        private readonly GymOrganizerContext db;

        public TestController(GymOrganizerContext db)
        {
            this.db = db;
        }

        [Route("test")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get()
        {
            //var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToArray();
            var users = db.Users.ToList();
            return Ok(new { message = "Hello API", users });
        }
    }

}
