using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precision.ViewModel
{
    public class CustomerListViewModel : IPageViewModel
    {

        public CustomerListViewModel()
        {
            PageName = "Customers";
        }

        public string PageName { get; set; }
    }
}
