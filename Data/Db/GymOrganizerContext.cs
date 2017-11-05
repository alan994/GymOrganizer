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
        public DbSet<Office> Offices { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProcessQueueHistory> ProcessQueuesHistory { get; set; }
        public GymOrganizerContext(DbContextOptions<GymOrganizerContext> options) : base(options)
        {
        }

        private string _connectionString = null;

        public GymOrganizerContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!string.IsNullOrEmpty(this._connectionString))
            {
                optionsBuilder.UseSqlServer(this._connectionString);
            }
        }
    }
}
