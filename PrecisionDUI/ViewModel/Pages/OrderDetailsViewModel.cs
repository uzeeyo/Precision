using Precision.Model;
using Precision.Services;
using System.Collections.Generic;

namespace Precision.ViewModel
{
    public class OrderDetailsViewModel : ObservableObject, IPageViewModel
    {
        private List<Product> _products;

        public OrderDetailsViewModel(Card card)
        {
            PageName = "Order Details";
            var oda = new OrderDataAccess();
            OrderDetails = oda.GetOrderDetailsByID(card.ID);
            //Products = OrderDetails.Products;
        }

        public string PageName { get; set; }
        public Order OrderDetails { get; set; }

        public string TotalPrice
        {
            get
            {
                decimal p = 0;
                foreach (Product product in OrderDetails.Products)
                {
                    p += product.Price;
                }
                return $"{p:N2}";
            }
        }
    }
}
