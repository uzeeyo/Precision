using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Product : BaseModel
    {
        private int _id;
        private int _categoryID;
        private string _name;
        private double _price;
        private bool _taxable;

        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Taxable { get; set; }
    }
}
