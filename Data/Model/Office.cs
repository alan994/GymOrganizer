using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    [Table("Offices")]
    public class Office
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        public Guid TenantId { get; set; }
        public ExistanceStatus Status { get; set; }
        public Guid CityId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual City City { get; set; }
    }
}
