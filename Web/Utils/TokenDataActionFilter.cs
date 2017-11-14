using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Services;

namespace Web.Utils
{
    public class TokenDataActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || !(context.Controller is AuthController))
            {
                return;
            }

            var members = context.Controller.GetType().GetFields().Select(f => f.GetValue(context.Controller)).ToList();
            foreach(var memeber in members)
            {
                if(memeber is ServiceBase)
                {
                    (memeber as ServiceBase).UserId = (context.Controller as AuthController).UserId;
                    (memeber as ServiceBase).TenantId = (context.Controller as AuthController).TenantId;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

    }
}
