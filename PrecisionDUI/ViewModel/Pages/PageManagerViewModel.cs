using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;


namespace Precision.ViewModel
{
    public class PageManagerViewModel : ObservableObject
    {
        public PageManagerViewModel()
        {
            CurrentPageName = "Home";
            _instance = this;
        }

        #region Private Fields

        private ICommand _changePage;
        private static IPageViewModel _currentPage;
        private string _pageName;
        private static PageManagerViewModel _instance;
        private Dictionary<string, Func<IPageViewModel>> _pages = new Dictionary<string, Func<IPageViewModel>>()
        {
            { "Home", () => _currentPage = null },
            { "Customers", () => new CustomerListViewModel(_instance) },
            { "Orders", () => new OrderListViewModel(_instance) },
            { "Messages", () => new MessagesViewModel(_instance) },
            { "Invoices", ()  => null },
            { "Quotes", () => null }

        };

        #endregion

        #region Public Properties

        public IPageViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                }
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public string CurrentPageName
        {
            get => _pageName;
            set
            {
                _pageName = value;
                OnPropertyChanged(nameof(CurrentPageName));
            }
        }

        public ICommand ChangePage
        {
            get
            {
                if (_changePage == null)
                {
                    _changePage = new RelayCommand(
                        p => ChangePageViewModel((string)p),
                        p => (string)p != CurrentPageName
                        );
                }
                return _changePage;
            }
        }
        #endregion
        
        private void ChangePageViewModel(string pageName)
        {
            IPageViewModel nextPage = _pages[pageName]();
            PageChanger(nextPage);
        }
        
        public void PageChanger(IPageViewModel page)
        {
            CurrentPage = page;
            CurrentPageName = CurrentPage?.PageName;
        }
    }
}
