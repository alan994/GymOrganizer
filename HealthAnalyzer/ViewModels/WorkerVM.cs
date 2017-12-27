using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAnalyzer.ViewModels
{
    public class WorkerVM
    {
        public DateTime SentTime { get; set; }
        public DateTime? ProcessTime { get; set; }
        public int Status { get; internal set; }
        public Guid Id { get; internal set; }
    }
}
