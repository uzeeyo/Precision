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
        public CustomerEditViewModel(PageManagerViewModel instance, int id)
        {
            LoadCustomerDetails(id);
            PageName = "Customers / Customer Details / Edit";
            _instance = instance;
        }



        private ICommand _saveChanges;
        private PageManagerViewModel _instance;
        private ICommand _cancelCommand;

        public string PageName { get; set; }
        public ICommand SaveChangesCommand
        {
            get 
            {
                if (_saveChanges == null)
                {
                    _saveChanges = new RelayCommand(
                        p => Save(CustomerDetails)
                        );
                }
                return _saveChanges;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        p => ReturnToDetails()
                        );
                }
                return (_cancelCommand);
            }
        }

        public Customer CustomerDetails { get; set; }


        private void LoadCustomerDetails(int id)
        {
            CustomerDetails = CustomerDataAccess.GetCustomerDetailsByID(id);
        }
        private void Save(Customer customer)
        {
            CustomerDataAccess.EditCustomer(customer);
            MaterialDesignThemes.Wpf.DialogHost.Close(null);
            _instance.PageChanger(new CustomerDetailsViewModel(_instance, customer.CustomerID));
        }

        private void ReturnToDetails()
        {
            _instance.PageChanger(new CustomerDetailsViewModel(_instance, CustomerDetails.CustomerID));
        }

    }
}
