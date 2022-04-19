using System.Collections.Generic;
using System.Linq;
using Precision.Model;
using Precision.Services;

namespace Precision.ViewModel
{
    public class CustomerListViewModel : IPageViewModel
    {

        public CustomerListViewModel(object instance)
        {
            PageName = "Customers";
            LoadCustomerCards();
            _instance = instance;

        }

        private object _instance;


        public string PageName { get; set; }
        public List<Card> Cards { get; set; }

        #region Methods
        private void LoadCustomerCards()
        {
            var custList = CustomerDataAccess.GetAllCustomers();

            var cards = from c in custList
                        select new Card()
                        {
                            ID = c.CustomerID,
                            Header = $"{c.FirstName} {c.LastName}",
                            Line1 = c.PhoneNumberFormatted,
                            Line2 = c.EmailAddress
                        };
            Cards = cards.ToList();
        }
        #endregion

    }
}
