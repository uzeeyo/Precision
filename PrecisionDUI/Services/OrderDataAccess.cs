using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Precision.Model;
using System.Data.SqlClient;
using System.Windows;

namespace Precision.Services
{
    class OrderDataAccess : DataAccessBase
    {
        //TODO: Figure out what to include in Order Cards. Possibly add Name and other info to Order models
        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            string query = "SELECT Orders.OrderID, Orders.CustomerID FROM Orders";

            using (var cmd = new SqlCommand(query, conString))
            {
                try
                {
                    conString.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var order = new Order();
                        order.CustomerID = (int)reader["CustomerID"];
                        order.OrderID = (int)reader["OrderID"];
                        orders.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
                finally
                {
                    conString.Close();
                }
            }
            return orders;
        }
    }
}
