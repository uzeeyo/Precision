using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Precision.Model;

namespace Precision.ViewModel
{
    public class CustomerDetailsViewModel : IPageViewModel
    {
        public CustomerDetailsViewModel(PageManagerViewModel instance, int id)
        {
            _instance = instance;
            PageName = "Customers / Customer Details";
            LoadDetails(id);
        }

        private PageManagerViewModel _instance;

        public string PageName { get; set; }
        public Customer CustomerDetails { get; set; }
        public List<Ticket> Tickets { get; set; }
        


        private void LoadDetails(int id)
        {
            CustomerDetails = Services.CustomerDataAccess.GetCustomerDetailsByID(id);
            Tickets = Services.TicketDataAccess.GetAllTicketsByCustomer(id);
        }

    }
}
