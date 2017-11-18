using Data.Enums;
using System;

namespace Web.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public ExistanceStatus Status { get; set; }        
    }
}