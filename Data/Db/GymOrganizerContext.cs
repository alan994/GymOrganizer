using Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.Db
{
    public class GymOrganizerContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Tenant> Tenants { get; set; }
        public GymOrganizerContext(DbContextOptions<GymOrganizerContext> options) : base(options)
        {
        }        
    }
}
