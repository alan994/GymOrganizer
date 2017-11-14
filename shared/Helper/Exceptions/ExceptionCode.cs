using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public enum ExceptionCode
    {
        MissingQueueData = 1,
        CityHasRelatedOffice = 2,
        CountryHasRelatedCity = 3,
        OfficeHasRelatedTerm = 4,
        NameAlreadyExists = 5
    }
}
