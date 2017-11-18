using Data.Enums;
using Data.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TenantVM
    {        
        public Guid Id { get; set; }        
        public string Name { get; set; }
        
        public string Settings { get; set; }
        public TenantConfiguration Configuration { get; set; }
        public ExistenceStatus Status { get; set; }
    }
}
