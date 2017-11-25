using Data.Enums;
using System;

namespace Web.ViewModels
{
    public class UserVM
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Owed { get; set; }
        public decimal Claimed { get; set; }
        public string TempPassword { get; set; }
        public string DisplayName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

        public bool IsAdmin { get; set; }
        public bool IsCoach { get; set; }
        public bool IsGlobalAdmin { get; set; }
        public ExistenceStatus Status { get; set; }
        public bool Active
        {
            get
            {
                return this.Status == ExistenceStatus.Active;
            }
            set
            {
                this.Status = value ? ExistenceStatus.Active : ExistenceStatus.Deactivated;
            }
        }
    }
}