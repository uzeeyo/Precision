using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Precision.Model;
using Precision.View;
using System.Windows.Input;
using Precision.Services;

namespace Precision.ViewModel
{
    public class OrderListViewModel : ObservableObject, IPageViewModel
    {

        public OrderListViewModel(PageManagerViewModel pageChangerInstance)
        {
            PageName = "Orders";
            LoadOrderCards();
            _pageChangerInstance = pageChangerInstance;
        }

        #region Private Fields
        private ICommand _editCommand;
        private ICommand _viewCommand;
        private PageManagerViewModel _pageChangerInstance;

        #endregion

        #region Public Properties
        public string PageName { get; set; }
        public List<Card> Cards { get; set; }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        p => EditOrder()
                        );
                }
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new RelayCommand(
                        p => ViewOrder((Card)p),
                        p => p is Card
                        );
                }
                return _viewCommand;
            }
        }
        #endregion

        #region Methods
        private void LoadOrderCards()
        {
            var orderList = OrderDataAccess.GetAllOrderCards();
            var orders = from o in orderList
                         select new Card()
                         {
                             ID = o.OrderID,
                             Header = $"Order# {o.OrderID}",
                             Line1 = $"{o.FirstName} {o.LastName}",
                             Line2 = o.PhoneNumber
                         };
            Cards = orders.ToList();

        }

        private void ViewOrder(Card card)
        {
            _pageChangerInstance.PageChanger(new OrderDetailsViewModel(card));
        }

        private void EditOrder()
        {

        }

        #endregion


    }
}
