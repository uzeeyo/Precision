using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageURL { get; set; }
        public DateTime Date { get; set; }
        public string MessageType { get; set; }

    }
}
