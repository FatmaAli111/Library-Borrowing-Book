using Data.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reminder : BaseEntity 
    {
        public int CheckoutID { get; set; }
        public Checkout Checkout { get; set; } = new Checkout();

        public DateTime SentDate { get; set; }
        public string Message { get; set; } = string.Empty;

        public ReminderType ReminderType { get; set; }
    }

}
