using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Precision.Model;

namespace Precision.ViewModel
{
    public class OrderListViewModel : ObservableObject, IPageViewModel
    {
        private int _orderID;
        private double _price;

        public OrderListViewModel()
        {
            PageName = "Orders";
        }

        public string PageName { get; set; }
        public int OrderID { get { return _orderID; } }
        public double Price { get { return _price; } }



    }
}
