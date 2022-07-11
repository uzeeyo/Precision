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
using Precision.Events;
using Force.DeepCloner;
using System.Windows.Controls;

namespace Precision.ViewModel
{
    public class OrderListViewModel : ObservableObject, IPageViewModel
    {

        public OrderListViewModel(PageManagerViewModel instance)
        {
            PageName = "Orders";
            LoadOrders();
            _instance = instance;
        }

        #region Private Fields
        private ICommand _editCommand;
        private ICommand _viewCommand;
        private ICommand _deleteCommand;
        private ICommand _searchCommand;
        private ICommand _showCalendarCommand;
        private PageManagerViewModel _instance;
        private string? _dateSearchText;
        #endregion

        #region Public Properties
        public string PageName { get; set; }
        public ObservableCollection<Order> OriginalOrders { get; set; }
        public ObservableCollection<Order> FilteredOrders { get; set; }
        public string IDSearchBox { get; set; } = "";
        public string CustomerSearchBox { get; set; } = "";
        public string? DateSearchText
        {
            get { return _dateSearchText; }
            set
            {
                if (_dateSearchText != value)
                {
                    _dateSearchText = value;
                    OnPropertyChanged(nameof(DateSearchText));
                }
            }
        }
        public IEnumerable<DateTime> Dates { get; set; }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        p => EditOrder((Order)p)
                        );
                }
                return _editCommand;
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new RelayCommand(
                        p => ViewOrder((Order)p)
                        );
                }
                return _viewCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand ??= new RelayCommand(
                    p => DeleteOrder((Order)p)
                    );
                return _deleteCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                _searchCommand ??= new RelayCommand(p => SearchOrders());
                return _searchCommand;
            }
        }

        public ICommand ShowCalendarCommand
        {
            get
            {
                _showCalendarCommand ??= new RelayCommand(p => ShowCalendar());
                return _showCalendarCommand;
            }
        }
        #endregion

        #region Methods
        private void LoadOrders()
        {
            OriginalOrders = new ObservableCollection<Order>(OrderDataAccess.GetAllOrders());
            FilteredOrders = OriginalOrders.DeepClone();
            OnPropertyChanged(nameof(FilteredOrders));
        }

        private void ViewOrder(Order order)
        {
            _instance.PageChanger(new OrderDetailsViewModel(order.OrderID));
        }

        private void EditOrder(Order order)
        {
            _instance.PageChanger(new OrderDetailsViewModel(order.OrderID) { EditMode = true });
        }

        private void DeleteOrder(Order order)
        {
            OrderDataAccess.DeleteOrderByID(order.OrderID);
            LoadOrders();
        }

        private void SearchOrders()
        {
            FilteredOrders.Clear();
            foreach (var o in OriginalOrders)
            {
                if (o.OrderID.ToString().Contains(IDSearchBox) 
                    && o.Customer.FullName.ToUpper().Contains(CustomerSearchBox.ToUpper())
                    && Dates.ElementAt(0) <= o.CreatedAt
                    && Dates.ElementAt(Dates.Count() - 1) >= o.CreatedAt)
                {
                    FilteredOrders.Add(o);
                }
            }
        }

        private void ShowCalendar()
        {
            var c = new CalendarViewModel();
            c.SelectedDateChanged += DateChanged;
            MaterialDesignThemes.Wpf.DialogHost.Show(new View.Dialogs.CalendarDialog()
            {
                DataContext = c
            });            
        }

        private void DateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            Dates = e.DateRange;
            DateSearchText = $"{Dates.ElementAt(0).Date.ToShortDateString()}-{Dates.ElementAt(e.DateRange.Count() - 1).Date.ToShortDateString()}";
        }

        #endregion


    }
}
