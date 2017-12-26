using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Helper.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class TenantLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<TenantLogic> logger;

        public TenantLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<TenantLogic>();
        }

        public async Task<Guid> AddTenant(TenantQueue tenantQueue)
        {
            await CheckAddEdit(tenantQueue);
            Tenant tenant = new Tenant()
            {
                Name = tenantQueue.Name,                
                Status = tenantQueue.Status                
            };

            this.db.Tenants.Add(tenant);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Tenant '{tenant.Name}' with id '{tenant.Id}' has successfully created", tenantQueue.TenantId.ToString(), tenantQueue.UserPerformingAction.ToString());
            return tenant.Id;
        }

        public async Task EditTenant(TenantQueue tenantQueue)
        {
            await CheckAddEdit(tenantQueue);

            Tenant tenant = await this.db.GetTenantById(tenantQueue.Id.Value).FirstOrDefaultAsync();
            tenant.Name = tenantQueue.Name;            
            tenant.Status = tenantQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogCustomInformation($"Tenant '{tenant.Name}' with id '{tenant.Id}' has successfully updated", tenantQueue.TenantId.ToString(), tenantQueue.UserPerformingAction.ToString());

        }
                
        private async Task CheckAddEdit(TenantQueue tenantQueue)
        {
            await CheckForSameName(tenantQueue.Name.Trim(), tenantQueue.Id);
        }

        private async Task CheckForSameName(string name, Guid? tenantId)
        {
            Tenant tenantWithTheSameName = null;
            if (tenantId.HasValue)
            {
                tenantWithTheSameName = await this.db
                    .GetAllTenants()
                    .Where(x => x.Name == name && x.Id != tenantId.Value)
                    .FirstOrDefaultAsync();
            }
            else
            {
                tenantWithTheSameName = await this.db
                    .GetAllTenants()
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();
            }

            if (tenantWithTheSameName != null)
            {
                throw new NameAlreadyExists($"Tenant ({tenantWithTheSameName.Id}) with the same name ({name}) already exists");
            }
        }
    }
}
