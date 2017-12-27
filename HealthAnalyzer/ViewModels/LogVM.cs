using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAnalyzer.ViewModels
{
    public class LogVM
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; internal set; }
        public int? ErrorCode { get; internal set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
        public string TenantId { get; set; }
    }
}
