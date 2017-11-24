using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class UserQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public ExistenceStatus Status { get; set; }
    }
}
