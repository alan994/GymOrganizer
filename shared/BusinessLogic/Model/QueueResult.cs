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
        public ProcessResult Result { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
    public class ProcessResult
    {
        public int? ExceptionCode { get; set; }
        public int OperationCode { get; set; }
    }

    public enum Status : int
    {
        Success = 1,
        Warning = 2,
        Fail = 3
    }
}
