using Data.Enums;
using System;

namespace BusinessLogic.Model
{
    public class CityQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public ExistanceStatus Status { get; set; }
        public Guid CountryId { get; set; }
    }
}
