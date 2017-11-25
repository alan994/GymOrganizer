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
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCoach { get; set; }
        public bool IsGlobalAdmin { get; set; }
        public string TempPassword { get; set; }
        public ExistenceStatus Status { get; set; }
        public decimal Owed { get; set; }
        public decimal Claimed { get; set; }
    }
}
