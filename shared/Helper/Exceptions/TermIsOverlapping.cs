using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class TermIsOverlapping : BusinessException
    {
        public TermIsOverlapping(string msg, Guid? termId = null, DateTime? start = null, DateTime? end = null, Guid? locationId = null, string locationName = null) : base(ExceptionCode.TermIsOverlapping, msg)
        {
            if (termId != null && termId.HasValue)
            {
                this.AdditionalData.Add("termId", termId.Value.ToString());
            }

            if (start != null && start.HasValue)
            {
                this.AdditionalData.Add("start", start.Value.ToString());
            }
            if (end != null && end.HasValue)
            {
                this.AdditionalData.Add("end", end.Value.ToString());
            }
            if (locationId != null && locationId.HasValue)
            {
                this.AdditionalData.Add("locationId", locationId.Value.ToString());
            }

            if (!string.IsNullOrEmpty(locationName))
            {
                this.AdditionalData.Add("locationName", locationName);
            }
        }
    }
}
