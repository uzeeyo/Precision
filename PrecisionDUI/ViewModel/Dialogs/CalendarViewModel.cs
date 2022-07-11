using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Precision.Events;

namespace Precision.ViewModel
{
    public class CalendarViewModel
    {
        private ICommand _datesChangedCommand;
        private ICommand _acceptDialog;

        public DateTime TodaysDate { get; set; } = DateTime.Now.Date;

        public IEnumerable<DateTime> SelectedDatesRange { get; set; }

        public ICommand DatesChangedCommand
        {
            get
            {
                _datesChangedCommand ??= new RelayCommand(
                    p => UpdatedSelectedDates(p)
                    );
                return _datesChangedCommand;
            }
        }

        public ICommand AcceptDialog
        {
            get
            {
                _acceptDialog ??= new RelayCommand(
                    p => CloseCalendar()
                    );
                return _acceptDialog;
            }
        }

        private void UpdatedSelectedDates(object o)
        {
            var v = o as SelectedDatesCollection;
            SelectedDatesRange = v.OrderBy(x => x.Date);

        }

        private void CloseCalendar()
        {
            var args = new SelectedDateChangedEventArgs();
            args.DateRange = SelectedDatesRange;
            OnDateChanged(args);
            MaterialDesignThemes.Wpf.DialogHost.Close(null);
        }

        public event EventHandler<SelectedDateChangedEventArgs> SelectedDateChanged;
        protected virtual void OnDateChanged(SelectedDateChangedEventArgs e)
        {
            SelectedDateChanged?.Invoke(this, e);
        }
        public delegate void SelectedDateChangedEventHandler(object sender, SelectedDateChangedEventArgs e);
    }
}
