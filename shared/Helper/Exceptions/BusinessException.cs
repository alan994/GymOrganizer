using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class BusinessException : Exception
    {
        public ExceptionCode Code { get; }
        public Dictionary<string, string> AdditionalData { get; set; }
        public BusinessException(ExceptionCode code, string msg) : base(msg)
        {
            this.AdditionalData = new Dictionary<string, string>();
            this.Code = code;
        }
    }
}
