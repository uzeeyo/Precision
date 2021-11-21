using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Order : BaseModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public double Price { get; set; }
        public List<Product> Products { get; set; }
        public string Notes { get; set; }
    }
}
