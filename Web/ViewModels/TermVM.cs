using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TermVM
    {
        public Guid Id { get; set; }        
        public DateTime Start { get; set; }        
        public DateTime End { get; set; }
        public int Capacity { get; set; }        
        public IntensityLevel IntensityLevel { get; set; }
        
        public OfficeVM Office { get; set; }
        public ExistanceStatus Status { get; set; }
        public UserVM Coach { get; set; }
    }
}
