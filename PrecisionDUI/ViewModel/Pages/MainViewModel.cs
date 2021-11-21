using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace Precision.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private ICommand _changePage;
        private IPageViewModel _currentPage;
        private string _pageName;
        private List<IPageViewModel> _pages;

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

        private void ChangePageViewModel(IPageViewModel page)
        {
            CurrentPage = page;
            CurrentPageName = page.PageName;
        }


    }
}
