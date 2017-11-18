using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class TenantQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public ExistenceStatus Status { get; set; }
        public string Name { get; set; }
        public string Settings { get; set; }
    }
}
