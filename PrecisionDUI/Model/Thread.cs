using System.Collections.Generic;

namespace Precision.Model
{
    public class Thread
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Message LastMessage { get; set; }
        public List<Message> Messages { get; set; }
            
    }
}
