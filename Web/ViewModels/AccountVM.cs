using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class AccountVM
    {
        public UserVM User { get; set; }
        public TenantVM Tenant { get; set; }
    }
}