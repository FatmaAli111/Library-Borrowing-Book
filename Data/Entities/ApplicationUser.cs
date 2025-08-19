using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
   
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public override string UserName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string MembershipType { get; set; } = string.Empty;
        public string MembershipStatus { get; set; } = string.Empty;
        public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
    }

}
