using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class AccountVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> Roles { get; set; }
    }
}