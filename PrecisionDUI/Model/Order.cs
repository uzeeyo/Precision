using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Precision.Model
{
    public class Order : BaseModel
    {
        private ObservableCollection<Product> _products;

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber {get; set;}
        public string EmailAddress { get; set; }
        public decimal Price { get; set; }
        public ObservableCollection<Product> Products
        {
            get { return _products; }

            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        public string Notes { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string TotalPrice
        {
            get
            {
                decimal p = 0;
                foreach (Product product in this.Products)
                {
                    p += product.Price;
                }
                return $"{p:N2}";
            }
        }
    }
}
