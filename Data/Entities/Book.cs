using Data.Entities.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        //public int TotalCopies { get; set; }
        //public int AvailableCopies { get; set; }
        //public int LostCopies { get; set; }
        //public int DamagedCopies { get; set; }
        //public int ReservedCopies { get; set; }
        //public int OnHoldCopies { get; set; }
        public int CategoryId { get; set; }             
        public Category Category { get; set; }
        public ICollection<BookCopy> BookCopies { get; set; }

    }

}
