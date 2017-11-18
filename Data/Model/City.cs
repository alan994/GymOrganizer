using Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    [Table("Cities")]
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public ExistenceStatus Status { get; set; }
        public virtual Country Country { get; set; }
    }
}
