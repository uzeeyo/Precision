using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Precision.Events
{
    public class SelectedDateChangedEventArgs : EventArgs
    {
        public DateTime? SelectedDate { get; set; }
        public IEnumerable<DateTime> DateRange { get; set; }
    }
}
