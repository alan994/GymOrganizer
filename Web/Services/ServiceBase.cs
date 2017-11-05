using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class ServiceBase
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
