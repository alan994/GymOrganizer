using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class TermLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<TermLogic> logger;

        public TermLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<TermLogic>();
        }

        public async Task AddCity(TermQueue termQueue)
        {
            await CheckAddEdit(termQueue);
            Term term = new Term()
            {
                Start = termQueue.Start,
                End = termQueue.End,
                Capacity = termQueue.Capacity,
                CoachId = termQueue.CoachId,
                IntensityLevel = termQueue.IntensityLevel,
                OfficeId = termQueue.OfficeId,
                Status = termQueue.Status,
                TenantId = termQueue.TenantId
            };

            this.db.Terms.Add(term);
            await this.db.SaveChangesAsync();

            this.logger.LogInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully created");
        }

        public async Task EditCity(TermQueue termQueue)
        {
            await CheckAddEdit(termQueue);

            Term term = await this.db.GetTermById(termQueue.TenantId, termQueue.Id.Value).FirstOrDefaultAsync();
            termQueue.Start = termQueue.Start;
            termQueue.End = termQueue.End;
            termQueue.Capacity = termQueue.Capacity;
            termQueue.CoachId = termQueue.CoachId;
            termQueue.IntensityLevel = termQueue.IntensityLevel;
            termQueue.OfficeId = termQueue.OfficeId;
            termQueue.Status = termQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully updated");

        }

        public async Task DeleteCity(TermQueue termQueue)
        {
            Term term = await this.db.GetTermById(termQueue.TenantId, termQueue.Id.Value).FirstOrDefaultAsync();

            this.db.Terms.Remove(term);
            await this.db.SaveChangesAsync();

            this.logger.LogInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully deleted");
        }

        private async Task CheckAddEdit(TermQueue termQueue)
        {
            CheckLogic.CheckDates(termQueue.Start, termQueue.End);
        }
    }
}
