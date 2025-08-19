using Data.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    namespace Data.Entities
    {
        public class BookCopy : BaseEntity
        {
            public int BookId { get; set; }
            public Book Book { get; set; }

            public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;

            public ICollection<Checkout> Checkouts { get; set; }
        }
    }

}
