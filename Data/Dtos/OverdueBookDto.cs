using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class OverdueBookDto
    {
        public string BookTitle { get; set; }
        public string UserName { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysOverdue { get; set; }
    }

}
