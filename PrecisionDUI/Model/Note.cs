using System;
using System.Collections.Generic;

namespace Precision.Model
{
    public class Note : ObservableObject
    {
        public int Id { get; set; }
        public List<string> NoteEntries { get; set; }
        public DateTime Time { get; set; }
    }
}
