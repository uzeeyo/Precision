using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Precision.Model
{
    public class Order : ObservableObject
    {
        public Order()
        {
            Customer = new Customer();
            Products = new ObservableCollection<Product>();
            Products.CollectionChanged += OnCollectionChanged;
        }

        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public decimal SubTotal
        {
            get
            {
                decimal st = 0;
                foreach (Product p in Products)
                {
                    st += p.FinalPrice;
                }
                return st;
            }
        }

        public decimal TaxTotal
        {
            get
            {
                decimal t = 0;
                foreach (Product p in Products)
                {
                    if (p.Tax != null)
                    {
                        t += p.Tax.Value;
                    }
                }
                return t;
            }
        }

        public decimal TotalPrice
        {
            get => SubTotal + TaxTotal;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Product p in e.OldItems)
                    p.PropertyChanged -= OnProductPriceChange;
            }
            if (e.NewItems != null)
            {
                foreach (Product p in e.NewItems)
                    p.PropertyChanged += OnProductPriceChange;
            }
        }

        private void OnProductPriceChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SubTotal));
            OnPropertyChanged(nameof(TaxTotal));
            OnPropertyChanged(nameof(TotalPrice));

        }

    }
}
