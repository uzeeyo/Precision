using Precision.Model;
using Precision.Services;
using System.Windows.Input;

namespace Precision.ViewModel
{
    public class OrderDetailsViewModel : ObservableObject, IPageViewModel
    {
        public OrderDetailsViewModel(Card card)
        {
            PageName = "Order Details";
            OrderDetails = OrderDataAccess.GetOrderDetailsByID(card.ID);
            OrderDetails.Products.Add(new Product());
        }

        private bool _editMode;
        private ICommand _editOrderCommand;

        #region Public Properties
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

        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                if (_editMode != value) { _editMode = value; }
                OnPropertyChanged(nameof(EditMode));
            }
        }

        public Customer Customer
        {
            get { return CustomerDataAccess.GetCustomerByOrderID(OrderDetails.OrderID); }
        }
        #endregion

        #region Commands
        public ICommand EditOrderCommand
        {
            get
            {
                if (_editOrderCommand == null)
                {
                    _editOrderCommand = new RelayCommand(
                        p => ChangeEditMode()
                        );
                }
                return _editOrderCommand;
            }
        }
        #endregion

        #region Methods
        private void ChangeEditMode()
        {
            if (EditMode == false)
            {
                EditMode = true;
            }
            else
            {
                EditMode= false;
            }
        }
        #endregion
    }
}
