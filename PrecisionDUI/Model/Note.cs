using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.Model
{
    public class Note
    {
        public int Id { get; set; }
        public List<string> NoteEntries { get; set; }
        public DateTime Time { get; set; }
    }
}
