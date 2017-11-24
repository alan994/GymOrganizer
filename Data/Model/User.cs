using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class User : IdentityUser<Guid>
    {   
        [StringLength(50)]
        public string FirstName { get; set; }       
        [StringLength(50)]
        public string LastName { get; set; }        
        public Guid TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public ExistenceStatus Status { get; set; }
    }
}
