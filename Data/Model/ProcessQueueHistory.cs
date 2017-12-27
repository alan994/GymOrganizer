using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class ProcessQueueHistory
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public DateTime AddedToQueue { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Guid? AddedById { get; set; }
        public string ErrorMesage { get; set; }
        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual User AddedBy { get; set; }
        public ResultStatus Status { get; set; }
        public ProcessType Type { get; set; }

        public ProcessQueueHistory()
        {
            this.AddedToQueue = DateTime.UtcNow;

        }
    }
}
