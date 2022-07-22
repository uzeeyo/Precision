using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Precision.Model
{
    public class Thread
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Message LastMessage { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public string Initial
        {
            get
            {
                return Customer.FirstName[0].ToString() + Customer.LastName[0].ToString();
            }
        }
            
    }
}
