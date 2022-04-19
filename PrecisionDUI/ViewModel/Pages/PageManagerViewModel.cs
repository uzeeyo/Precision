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
            Pages = new List<IPageViewModel>();
            Pages.Add(new OrderListViewModel(this));
            Pages.Add(new CustomerListViewModel(this));
            CurrentPageName = "Home";
        }

        #region Private Fields

        static ICommand _changePage;
        static ICommand _viewCommand;
        static IPageViewModel _currentPage;
        static string _pageName;

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

        public List<IPageViewModel> Pages { get; set; }
        public ICommand ChangePage
        {
            get
            {
                if (_changePage == null)
                {
                    _changePage = new RelayCommand(
                        p => ChangePageViewModel(Pages.Where(x => x.PageName == (string)p).FirstOrDefault()),
                        p => (string)p != CurrentPageName
                        );
                }
                return _changePage;
            }
        }




        #endregion
        public void ChangePageViewModel(IPageViewModel page) => PageChanger(page);

        //private void OpenOrderDetails(Card c) => this.PageChanger(new OrderDetailsViewModel(c));
        //private void OpenCustomerEdit(Card c) => this.PageChanger(new CustomerEditViewModel(c));

        public void PageChanger(IPageViewModel page)
        {
            CurrentPage = page;
            CurrentPageName = CurrentPage?.PageName;
        }
    }
}
