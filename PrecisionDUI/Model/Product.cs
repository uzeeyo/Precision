using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Precision.Model
{
    public class Product : BaseModel
    {
        private int _id;
        private int _categoryID;
        private string _name;
        private decimal _price;
        private bool _taxable;

        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal Price
        {
            get { return Math.Round(_price,2); }
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
            }

        }
        public bool Taxable { get; set; }
        public string PriceFormatted
        {
            get
            {
                return $"{Price:C}";
            }
        }
    }
}
