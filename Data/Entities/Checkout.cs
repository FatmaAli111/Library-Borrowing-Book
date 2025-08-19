using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    using global::Data.Entities.Data.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Checkout
    {
        public int CheckoutID { get; set; }


        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal FineAmount { get; set; } = 1;

        public bool IsOverdue { get; set; }
        public bool IsRenewed { get; set; }
        public bool IsLost { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsReserved { get; set; }
        public bool FinePaid { get; set; }
        public DateTime? FinePaidDate { get; set; }

        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
        public string UserID { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = new ApplicationUser();

        public int BookCopyID { get; set; }
        public BookCopy BookCopy { get; set; } 
    }

}
