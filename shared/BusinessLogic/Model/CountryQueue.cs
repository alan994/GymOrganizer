using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class CountryQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }
        public string NumericCode { get; set; }
        public ExistanceStatus Status { get; set; }

    }
}
