using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Precision.Model
{
    public class Product : ObservableObject
    {
        private decimal? _changedPrice;
        private decimal _finalPrice;
        private bool _taxable;

        public int ProductID { get; set; }
        public int EntryID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal? ChangedPrice
        {
            get => _changedPrice ?? Price;
            set
            {
                if (_changedPrice != value)
                {
                    _changedPrice = value;
                }
                OnPropertyChanged(nameof(ChangedPrice));
                OnPropertyChanged(nameof(FinalPrice));
                OnPropertyChanged(nameof(Tax));
            }

        }

        public decimal FinalPrice
        {
            get
            {
                _finalPrice = ChangedPrice ?? Price;
                return _finalPrice;
            }
        }

        public decimal? Tax
        {
            get { return Taxable ? Decimal.Multiply(FinalPrice, (decimal)0.07) : 0; }
        }

        public bool Taxable
        {
            get => _taxable;
            set
            {
                if (_taxable != value)
                {
                    _taxable = value;
                }
                OnPropertyChanged(nameof(Taxable));
                OnPropertyChanged(nameof(Tax));
            }
        }
    }
}
