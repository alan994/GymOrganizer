using Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    [Table("Terms")]
    public class Term
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public int Capacity { get; set; }
        public Guid CoachId { get; set; }
        public Guid OfficeId { get; set; }
        public IntensityLevel IntensityLevel { get; set; }
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Office Office { get; set; }
        public ExistenceStatus Status { get; set; }
        public virtual User Coach { get; set; }
    }
}
