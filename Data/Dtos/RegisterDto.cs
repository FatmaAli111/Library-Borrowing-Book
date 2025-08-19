using System.ComponentModel.DataAnnotations;

namespace Data.Dtos
{
    public class RegisterDto
    {
      
        public string FirstName { get; set; }

       
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string MembershipType { get; set; }

        public string MembershipStatus { get; set; }

    }
}
