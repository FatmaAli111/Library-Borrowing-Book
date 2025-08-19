using Data.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
   public class BookDto
    {

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public BookCopyStatus Status { get; set; }
        public int CategoryId { get; set; }

    }
}
