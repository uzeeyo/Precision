using System.Collections.Generic;

namespace Precision.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber {get; set;}
        public string EmailAddress { get; set; }
        public decimal Price { get; set; }
        public List<Product> Products { get; set; }
        public string Notes { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
