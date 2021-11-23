using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Card : BaseModel
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }

    }
}
