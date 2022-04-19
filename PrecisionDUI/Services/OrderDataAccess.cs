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
    static class OrderDataAccess
    {
        //TODO: Figure out what to include in Order Cards. Possibly add Name and other info to Order models
        public static List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            string query = "SELECT Orders.OrderID, Orders.CustomerID FROM Orders";
            var conn = DataAccessBase.conString;

            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
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
                    conn.Close();
                }
            }
            return orders;
        }

        public static List<Order> GetAllOrderCards()
        {
            var orders = new List<Order>();
            string query = "SELECT Orders.OrderID, Customers.CustomerID, Customers.FirstName, Customers.LastName, Customers.PhoneNumber " +
                "FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID " +
                "ORDER BY Orders.OrderID DESC";
            var conn = DataAccessBase.conString;


            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var order = new Order();
                            order.OrderID = (int)reader["OrderID"];
                            order.FirstName = $"{(string)reader["FirstName"]}"; 
                            order.LastName = $"{(string)reader["LastName"]}";
                            order.PhoneNumber = (string)reader["PhoneNumber"];
                            orders.Add(order);
                        }
                    }

                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                return orders;
            }
        }

        public static Order GetOrderDetailsByID(int id)
        {
            string query = "SELECT Order_Entries.OrderID, Products.Name, Products.Price, Products.Taxable FROM Order_Entries " +
                           "INNER JOIN Products ON Order_Entries.ProductID = Products.ProductID " +
                           "WHERE Order_Entries.OrderID = @id";
            var order = new Order();
            order.OrderID = id;
            var products = new List<Product>();
            var conn = DataAccessBase.conString;


            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        if (count == 0) { order.OrderID = (int)reader["OrderID"]; }
                        var product = new Product();
                        product.Name = (string)reader["Name"];
                        product.Price = (decimal)reader["Price"];
                        product.Taxable = reader.GetBoolean(reader.GetOrdinal("Taxable"));
                        products.Add(product);

                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            order.Products = products;
            return order;

        }
    }
}
