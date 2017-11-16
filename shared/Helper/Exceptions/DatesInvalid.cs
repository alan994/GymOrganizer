using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class DatesInvalid : BusinessException
    {
        public DatesInvalid(string msg, DateTime? start = null, DateTime? end = null) : base(ExceptionCode.DatesInvalid, msg)
        {
            if (start!= null && start.HasValue)
            {
                this.AdditionalData.Add("start", start.Value.ToString());
            }
            if (end != null && end.HasValue)
            {
                this.AdditionalData.Add("end", end.Value.ToString());
            }
        }
    }
}
