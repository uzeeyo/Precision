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
    public class MessagesViewModel : ObservableObject, IPageViewModel
    {
        public MessagesViewModel(PageManagerViewModel instance)
        {
            _instance = instance;
            PageName = "Messages";
            LoadThreads();
        }

        private readonly PageManagerViewModel _instance;
        private Thread _selectedThread;
        private Thread _currentThread;

        public string PageName { get; set; }
        public ObservableCollection<Thread> Threads { get; set; }
        public Thread SelectedThread
        {
            get => _selectedThread;
            set
            {
                if (_selectedThread != value)
                {
                    _selectedThread = value;
                    OnPropertyChanged(nameof(SelectedThread));
                    OpenThread(SelectedThread);
                }
            }
        }

        public Thread CurrentThread
        {
            get => _currentThread;
            set
            {
                if (_currentThread != value)
                {
                    _currentThread = value;
                    OnPropertyChanged(nameof(CurrentThread));
                }
            }
        }
        


        private void LoadThreads()
        {
            Threads = MessageDataService.LoadAllThreads();
        }

        private void OpenThread(Thread thread)
        {
            CurrentThread = MessageDataService.LoadThreadDetails(thread.Id);
        }

    }
}
