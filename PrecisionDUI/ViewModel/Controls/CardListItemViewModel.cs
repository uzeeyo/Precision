using Precision.Model;
using Precision.Services;
using System.Collections.Generic;
using System.Linq;

namespace Precision.ViewModel
{
    class CardListItemViewModel 
    {
        #region Private Fields


        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor to initialize properties based on the page type. 
        /// </summary>
        public CardListItemViewModel(string param)
        {
            switch (param)
            {
                case "Customers":
                    ListType = param;
                    LoadCustomerCards();
                    break;
                case "Orders":
                    ListType = param;
                    LoadOrderCards();
                    break;
                
            }
        }
        #endregion

        #region Public Properties
        public List<Card> Cards { get; set; }
        public string ListType { get; set; }
        #endregion

        #region Methods

        private void LoadCustomerCards()
        {
            var cda = new CustomerDataAccess();
            var custList = cda.GetAllCustomers();

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

        private void LoadOrderCards()
        {
            var oda = new OrderDataAccess();
            var orderList = oda.GetAllOrderCards();
            Cards = orderList;

        }

        private void ViewOrderDetails(int id)
        {

        }

        #endregion
    }
}
