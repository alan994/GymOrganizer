﻿using BusinessLogic.Model;
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

        public async Task<Guid> AddTerm(TermQueue termQueue)
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
                Price = termQueue.Price,
                TenantId = termQueue.TenantId
            };

            this.db.Terms.Add(term);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully created", termQueue.TenantId.ToString(), termQueue.UserPerformingAction.ToString());
            return term.Id;
        }

        public async Task EditTerm(TermQueue termQueue)
        {
            await CheckAddEdit(termQueue);

            Term term = await this.db.GetTermById(termQueue.TenantId, termQueue.Id.Value).FirstOrDefaultAsync();
            term.Start = termQueue.Start;
            term.End = termQueue.End;
            term.Capacity = termQueue.Capacity;
            term.CoachId = termQueue.CoachId;
            term.Price = termQueue.Price;
            term.IntensityLevel = termQueue.IntensityLevel;
            term.OfficeId = termQueue.OfficeId;
            term.Status = termQueue.Status;

            await this.db.SaveChangesAsync();
            this.logger.LogCustomInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully updated", termQueue.TenantId.ToString(), termQueue.UserPerformingAction.ToString());

        }

        public async Task DeleteTerm(TermQueue termQueue)
        {
            Term term = await this.db.GetTermById(termQueue.TenantId, termQueue.Id.Value).FirstOrDefaultAsync();

            this.db.Terms.Remove(term);
            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"Term '{term.Start.ToString()} - {term.End.ToString()}' with id '{term.Id}' has successfully deleted", termQueue.TenantId.ToString(), termQueue.UserPerformingAction.ToString());
        }

        private async Task CheckAddEdit(TermQueue termQueue)
        {
            CheckLogic.CheckDates(termQueue.Start, termQueue.End);
            await CheckIfTermsAreOverlapping(termQueue);
        }

        private async Task CheckIfTermsAreOverlapping(TermQueue termQueue)
        {
            var overlappingTerm = await this.db.Terms
                .Where(x => x.OfficeId == termQueue.OfficeId && x.Status != Data.Enums.ExistenceStatus.Deleted && x.Start < termQueue.End && termQueue.Start < x.End)
                .Include(x => x.Office)
                .FirstOrDefaultAsync();

            if(overlappingTerm != null)
            {
                throw new TermIsOverlapping($"Term ({termQueue.Id ?? Guid.Empty}) that is starting {termQueue.Start.ToString()} and ending {termQueue.End.ToString()} in office ({termQueue.OfficeId}) is ovelapping with another term.",
                    overlappingTerm.Id, overlappingTerm.Start, overlappingTerm.End, overlappingTerm.OfficeId, overlappingTerm.Office.Name);
            }
        }
    }
}
