using Microsoft.AspNetCore.Identity;
using System;

namespace Data.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
