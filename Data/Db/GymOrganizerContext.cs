using Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
                        

            #region DefaultValues
            builder.Entity<Office>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<City>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<Country>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<ProcessQueueHistory>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<Tenant>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<Term>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            #endregion
        }
    }
}
