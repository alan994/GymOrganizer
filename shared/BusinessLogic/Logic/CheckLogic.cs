using Helper.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Logic
{
    public static class CheckLogic
    {
        public static void CheckDates(DateTime start, DateTime end, string exceptionMessage = "Start date can't be bigger then end date")
        {
            if(start > end)
            {
                throw new DatesInvalid(exceptionMessage, start, end);
            }
        }
    }
}
