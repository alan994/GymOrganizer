using System;

namespace Helper.Exceptions
{
    public class CityHasRelatedOffice : BusinessException
    {
        public CityHasRelatedOffice(string msg, Guid? officeId = null, string officeName = null) : base(ExceptionCode.CityHasRelatedOffice, msg)
        {
            if(officeId != null && officeId.HasValue)
            {
                this.AdditionalData.Add("officeId", officeId.Value.ToString());
            }
            if (!string.IsNullOrEmpty(officeName))
            {
                this.AdditionalData.Add("officeName", officeName);
            }
        }
    }
}
