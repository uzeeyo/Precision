using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Precision.Model;
using Precision.Services;

namespace Precision.ViewModel
{
    public class MessagesViewModel : IPageViewModel
    {
        public MessagesViewModel(PageManagerViewModel _instance)
        {
            _instance = _instance;
            PageName = "Messages";
            LoadThreads();
        }

        private IPageViewModel _instance;

        public string PageName { get; set; }
        public ObservableCollection<Thread> Threads { get; set; }


        private void LoadThreads()
        {
            Threads = MessageDataService.LoadAllThreads();
        }

    }
}
