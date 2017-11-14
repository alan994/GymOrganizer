using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Exceptions
{
    public class NameAlreadyExists : BusinessException
    {
        public NameAlreadyExists(string msg, Guid? objectId = null, string name = null) : base(ExceptionCode.NameAlreadyExists, msg)
        {
            if (objectId != null && objectId.HasValue)
            {
                this.AdditionalData.Add("objectId", objectId.Value.ToString());
            }
            if (!string.IsNullOrEmpty(name))
            {
                this.AdditionalData.Add("name", name);
            }
        }
    }
}
