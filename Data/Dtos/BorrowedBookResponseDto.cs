using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class BorrowedBookResponseDto
    {
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
        public decimal FineAmount { get; set; }
    }

}
