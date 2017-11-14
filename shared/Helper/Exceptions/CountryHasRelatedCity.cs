using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class CountryHasRelatedCity : BusinessException
    {
        public CountryHasRelatedCity(string msg, Guid? cityId = null, string cityName = null) : base(ExceptionCode.CountryHasRelatedCity, msg)
        {
            if (cityId != null && cityId.HasValue)
            {
                this.AdditionalData.Add("cityId", cityId.Value.ToString());
            }
            if (!string.IsNullOrEmpty(cityName))
            {
                this.AdditionalData.Add("cityName", cityName);
            }
        }
    }
}
