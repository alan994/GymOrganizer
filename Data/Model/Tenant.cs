using Data.Enums;
using Data.Model.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Settings { get; set; }
        public ExistenceStatus Status { get; set; }
        [NotMapped]
        public TenantConfiguration Configuration
        {
            get
            {
                return JsonConvert.DeserializeObject<TenantConfiguration>(this.Settings);
            }
        }

    }
}
