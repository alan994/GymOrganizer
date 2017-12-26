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
    public class OfficeLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<OfficeLogic> logger;

        public OfficeLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<OfficeLogic>();
        }

        public async Task<Guid> AddOffice(OfficeQueue officeQueue)
        {
            await CheckAddEdit(officeQueue);

            Office office = new Office()
            {
                Name = officeQueue.Name,
                Address = officeQueue.Address,
                CityId = officeQueue.CityId,
                Status = officeQueue.Status,                
                TenantId = officeQueue.TenantId
            };

            this.db.Offices.Add(office);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Office '{office.Name}' with id '{office.Id}' has successfully created", officeQueue.TenantId.ToString(), officeQueue.UserPerformingAction.ToString());
            return office.Id;
        }

        public async Task EditOffice(OfficeQueue officeQueue)
        {
            await CheckAddEdit(officeQueue);

            Office office = await this.db.GetOfficeById(officeQueue.TenantId, officeQueue.Id.Value).FirstOrDefaultAsync();
            office.Name = officeQueue.Name;
            office.Address = officeQueue.Address;
            office.CityId = officeQueue.CityId;
            office.Status = officeQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogCustomInformation($"Office '{office.Name}' with id '{office.Id}' has successfully updated", officeQueue.TenantId.ToString(), officeQueue.UserPerformingAction.ToString());

        }

        public async Task DeleteOffice(OfficeQueue officeQueue)
        {
            await CheckDelete(officeQueue);
            Office office = await this.db.GetOfficeById(officeQueue.TenantId, officeQueue.Id.Value).FirstOrDefaultAsync();
            
            this.db.Offices.Remove(office);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Office '{office.Name}' with id '{office.Id}' has successfully deleted", officeQueue.TenantId.ToString(), officeQueue.UserPerformingAction.ToString());
        }

        private async Task CheckAddEdit(OfficeQueue officeQueue)
        {
            await CheckForSameName(officeQueue.Name.Trim(), officeQueue.Id, officeQueue.TenantId);
        }

        private async Task CheckForSameName(string name, Guid? officeId, Guid tenantId)
        {
            Office officeWithTheSameName = null;
            if (officeId.HasValue)
            {
                officeWithTheSameName = await this.db
                    .GetAllOffices(tenantId)
                    .Where(x => x.Name == name && x.Id != officeId.Value)
                    .FirstOrDefaultAsync();
            }
            else
            {
                officeWithTheSameName = await this.db
                    .GetAllOffices(tenantId)
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();
            }

            if (officeWithTheSameName != null)
            {                
                throw new NameAlreadyExists($"Office ({officeWithTheSameName.Id}) with the same name ({name}) already exists");
            }
        }

        private async Task CheckDelete(OfficeQueue officeQueue)
        {
            var relatedTerms = await this.db.GetAllTermsForOffice(officeQueue.TenantId, officeQueue.Id.Value).FirstOrDefaultAsync();
            if (relatedTerms != null)
            {
                throw new OfficeHasRelatedTerm($"Office ({officeQueue.Id}) has related term ({relatedTerms.Id})", relatedTerms.Id, relatedTerms.Start, relatedTerms.End);
            }
        }
    }
}
