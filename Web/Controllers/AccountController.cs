using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/account")]
    //[Authorize]
    public class AccountController : Controller
    {
        [Authorize]
        public async Task<IActionResult> GetAccountInfo()
        {
            //TODO: implement features
            var vm = new AccountVM()
            {
                DisplayName = "Alan Jagar",
                FirstName = "Alan",
                LastName = "Jagar",
                UserId = Guid.Parse("B7025896-51CE-4564-A392-6B9C9CBC524E"),
                Roles = new List<Guid>()
                {
                    Guid.Parse("B6AF902D-68F5-41E6-9069-EFA486430D51"),
                    Guid.Parse("B6AF902D-68F5-41E6-9069-EFA486430D52"),
                    Guid.Parse("B6AF902D-68F5-41E6-9069-EFA486430D53")
                }
            };

            return Json(vm);
        }
    }
}
