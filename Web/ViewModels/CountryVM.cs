using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CountryVM
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }
        public string NumericCode { get; set; }
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
//id: null, name: "Dsf", iso2Code: "s", iso3Code: "df", numericCode: "dsfds", active: true