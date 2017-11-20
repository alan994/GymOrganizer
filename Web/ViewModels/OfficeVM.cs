using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OfficeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public CityVM City { get; set; }
        public ExistenceStatus Status { get; set; }
        public bool Active
        {
            get
            {
                return this.Status == ExistenceStatus.Active;
            }
            set
            {
                this.Status = value ? ExistenceStatus.Active : ExistenceStatus.Deactivated;
            }
        }
    }
}
