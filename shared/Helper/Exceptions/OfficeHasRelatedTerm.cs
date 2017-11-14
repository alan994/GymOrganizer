using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class OfficeHasRelatedTerm : BusinessException
    {
        public OfficeHasRelatedTerm(string msg, Guid? termId = null, DateTime? termStart = null, DateTime? termEnd = null) : base(ExceptionCode.OfficeHasRelatedTerm, msg)
        {
            if (termId != null && termId.HasValue)
            {
                this.AdditionalData.Add("termId", termId.Value.ToString());
            }

            if (termStart != null && termStart.HasValue)
            {
                this.AdditionalData.Add("termStart", termStart.Value.ToString());
            }

            if (termEnd != null && termEnd.HasValue)
            {
                this.AdditionalData.Add("termEnd", termEnd.Value.ToString());
            }

        }
    }
}
