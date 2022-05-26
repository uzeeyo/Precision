using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Precision.Model
{
    public class Product : BaseModel
    {
        private decimal _price;

        public int ProductID { get; set; }
        public int EntryID { get; set; }
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
