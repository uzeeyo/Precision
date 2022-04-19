using Precision.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Precision.Services;

namespace Precision.ViewModel
{
    public class CustomerEditViewModel : IPageViewModel
    {
        public CustomerEditViewModel(Card card)
        {
            CustomerDetails = CustomerDataAccess.GetCustomerByID(card.ID);
        }

        private ICommand _saveChanges;
        public string PageName { get; set; }
        public ICommand SaveChanges
        {
            get => _saveChanges;
            set
            {
                if (_saveChanges == null)
                {
                    _saveChanges = new RelayCommand(
                        p => Save()
                        );
                }
            }
        }

        public Customer CustomerDetails { get; set; }



        private void Save()
        {
            if (CustomerDetails == null)
            {
                //var cd = new CustomerDataAccess();
            }
        }

    }
}
