using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Utils
{
    public class UserDataActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var members = context.Controller.GetType().GetFields().Select(f => f.GetValue(context.Controller)).ToList();
            foreach(var memeber in members)
            {
                if(memeber is ServiceBase)
                {
                    (memeber as ServiceBase).UserId = Guid.NewGuid();
                    (memeber as ServiceBase).TenantId = Guid.NewGuid();
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

    }
}
