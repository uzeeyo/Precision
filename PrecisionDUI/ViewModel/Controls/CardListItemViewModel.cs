using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Precision.Services;
using Precision.Model;

namespace Precision.ViewModel
{
    class CardListItemViewModel
    {
        /// <summary>
        /// Default constructor to initialize properties
        /// </summary>
        public CardListItemViewModel()
        {
            LoadCustomerCards();
        }

        #region Public Properties
        public List<Card> Cards { get; set; }
        #endregion

        public void LoadCustomerCards()
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


    }
}
