using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class TermQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Capacity { get; set; }
        public Guid CoachId { get; set; }
        public Guid OfficeId { get; set; }
        public IntensityLevel IntensityLevel { get; set; }
        public ExistenceStatus Status { get; set; }
        public decimal Price { get; internal set; }
    }
}
