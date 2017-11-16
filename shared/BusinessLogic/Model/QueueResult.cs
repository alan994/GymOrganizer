using Data.Enums;
using Helper.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class QueueResult
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public Status Status { get; set; }
        public Guid RequestOueueId { get; set; }
        public ExceptionCode? ExceptionCode { get; set; }
        public ProcessType ProcessType { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }

        public QueueResult(ProcessType processType)
        {
            this.AdditionalData = new Dictionary<string, string>();
            this.ProcessType = processType;
        }
    }    
    public enum Status : int
    {
        Success = 1,
        Warning = 2,
        Fail = 3
    }
}
