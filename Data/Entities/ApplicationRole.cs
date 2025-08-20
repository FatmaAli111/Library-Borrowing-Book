using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{

        public class ApplicationRole : IdentityRole
        {
            public bool IsDefault { get; set; }
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }

    }


}
