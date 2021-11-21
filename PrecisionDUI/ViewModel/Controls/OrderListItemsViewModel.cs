using Precision.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Precision.ViewModel
{
    public class OrderListItemsViewModel : ObservableObject
    {
        private List<Product> _products;
        private ICommand _viewCommand;
        private double _totalPrice;

        public OrderListItemsViewModel()
        {
            Products = new List<Product>();
        }

        public List<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != null)
                {
                    _products = null;
                }
                OnPropertyChanged(nameof(Products));
            }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                foreach (Product p in Products)
                {
                    _totalPrice += p.Price;
                }
            }
        }

        public ICommand ViewCommand
        {
            get { return _viewCommand; }
        }


    }
}
