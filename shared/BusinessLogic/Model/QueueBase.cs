using System;

namespace BusinessLogic.Model
{
    public class QueueBase
    {
        public Guid UserPerformingAction { get; set; }
        public Guid TenantId { get; set; }
    }
}
