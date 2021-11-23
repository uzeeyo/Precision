using Precision.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Precision.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Private Fields

        private ICommand _changePage;
        private ICommand _viewCommand;
        private IPageViewModel _currentPage;
        private string _pageName;

        #endregion

        /// <summary>
        /// Default constructor sets initial page to Home
        /// </summary>
        public MainViewModel()
        {
            Pages = new List<IPageViewModel>();
            Pages.Add(new OrderListViewModel());
            Pages.Add(new CustomerListViewModel());
            CurrentPageName = "Home";
        }


        public IPageViewModel CurrentPage
        {
            get { return _currentPage; }
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
            get { return _pageName; }
            set
            {
                _pageName = value;
                OnPropertyChanged(nameof(CurrentPageName));
            }
        }
        
        public List<IPageViewModel> Pages { get; set; }
        public ICommand ChangePage
        {
            get
            {
                if (_changePage == null)
                {
                    _changePage = new RelayCommand(
                        p => ChangePageViewModel(Pages.Where(x => x.PageName == (string)p).Single()),
                        p => (string)p != CurrentPageName 
                        );
                }
                return _changePage;
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new RelayCommand(
                        p => OpenOrderDetails((Card)p),
                        p => p is Card
                        );
                }
                return _viewCommand;
            }
        }

        private void ChangePageViewModel(IPageViewModel page)
        {
            CurrentPage = page;
            CurrentPageName = CurrentPage.PageName;
        }

        private void OpenOrderDetails(Card c)
        {
            CurrentPage = new OrderDetailsViewModel(c);
            CurrentPageName = CurrentPage.PageName;
        }
    }
}
