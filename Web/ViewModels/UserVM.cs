﻿using Data.Enums;
using System;

namespace Web.ViewModels
{
    public class UserVM
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
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