using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Ticket
    {
        public Ticket()
        {
            Devices = new List<Device>();
            Notes = new List<Note>();
        }
        public int TicketID { get; set; }
        public List<Device> Devices { get; set; }
        public int InvoiceID;
        public List<Note> Notes { get; set; }
        public DateTime DateCreated { get; set; }


    }
}
