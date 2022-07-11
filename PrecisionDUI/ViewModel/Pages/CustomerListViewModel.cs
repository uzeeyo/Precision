using System.Collections.Generic;
using System.Linq;
using Precision.Model;
using Precision.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Force.DeepCloner;

namespace Precision.ViewModel
{
    public class CustomerListViewModel : BaseModel, IPageViewModel
    {

        public CustomerListViewModel(PageManagerViewModel instance)
        {
            PageName = "Customers";
            LoadCustomerCards();
            _instance = instance;;
        }

        private PageManagerViewModel _instance;
        private ICommand _viewCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _searchCommand;
        private string _nameSearchBox = "";
        private string _emailSearchBox = "";
        private string _phoneSearchBox = "";
        private string _addressSearchBox = "";

        public string PageName { get; set; }
        public ObservableCollection<Customer> FilteredCustomers { get; set; }
        public ObservableCollection<Customer> OriginalCustomers { get; set; }
        public string NameSearchBox
        {
            get { return _nameSearchBox; }
            set { _nameSearchBox = value; }
        }
        public string PhoneSearchBox
        {
            get { return _phoneSearchBox; }
            set { _phoneSearchBox = value; }
        }
        public string EmailSearchBox
        {
            get { return _emailSearchBox; }
            set { _emailSearchBox = value; }
        }

        public string AddressSearchBox
        {
            get { return _addressSearchBox; }
            set { _addressSearchBox = value; }
        }
        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new RelayCommand(
                        p => OpenViewPage((Customer)p),
                        p => p is Customer
                        );
                }
                return _viewCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        p => OpenEditPage((Customer)p),
                        p => p is Customer
                        );
                }
                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        p => ConfirmDelete((Customer)p),
                        p => p is Customer
                        );
                }
                return (_deleteCommand);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(
                        p => SearchList()
                        );
                }
                return _searchCommand;
            }
        }

        #region Methods
        private void LoadCustomerCards()
        {
            var custList = CustomerDataAccess.GetAllCustomers();

            var customers = from c in custList
                        select new Customer()
                        {
                            CustomerID = c.CustomerID,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            PhoneNumber = c.PhoneNumber,
                            EmailAddress = c.EmailAddress
                        };
            OriginalCustomers = new ObservableCollection<Customer>(customers);
            FilteredCustomers = OriginalCustomers.DeepClone();
            OnPropertyChanged(nameof(FilteredCustomers));
        }

        private void OpenViewPage(Customer c)
        {
            _instance.PageChanger(new CustomerDetailsViewModel(_instance, c.CustomerID));
        }
        private void OpenEditPage(Customer c)
        {
            _instance.PageChanger(new CustomerEditViewModel(_instance, c.CustomerID));
        }

        private void ConfirmDelete(Customer customer)
        {
            var dialog = new View.Dialogs.ConfirmDeleteDialog()
            {
                DataContext = new
                {
                    Id = customer.CustomerID,
                    ConfirmDeleteCommand = new RelayCommand(p => DeleteCustomer(customer.CustomerID))

                }
            };
            
            MaterialDesignThemes.Wpf.DialogHost.Show(dialog);
        }

        private void DeleteCustomer(int id)
        {
            CustomerDataAccess.RemoveCustomer(id);
            LoadCustomerCards();
            MaterialDesignThemes.Wpf.DialogHost.Close(null);
        }

        private void SearchList()
        {
            FilteredCustomers.Clear();
            foreach (var c in OriginalCustomers)
            {
                if (c.FullName.ToUpper().Contains(NameSearchBox.ToUpper())
                    && c.EmailAddress.ToUpper().Contains(EmailSearchBox.ToUpper())
                    && c.PhoneNumber.Contains(PhoneSearchBox)
                    && c.AddressFormatted.ToUpper().Contains(AddressSearchBox.ToUpper())
                    )
                {
                    FilteredCustomers.Add(c);
                }
            }
        }

        #endregion




    }
}
