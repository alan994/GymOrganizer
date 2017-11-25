using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {

        }

        public Role(string name): base(name)
        {

        }
    }
}
