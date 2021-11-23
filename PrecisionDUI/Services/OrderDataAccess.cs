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

        public List<Card> GetAllOrderCards()
        {
            var cards = new List<Card>();
            string query = "SELECT Orders.OrderID, Customers.CustomerID, Customers.FirstName, Customers.LastName, Customers.PhoneNumber " +
                "FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID " +
                "ORDER BY Orders.OrderID DESC";

            using (var cmd = new SqlCommand(query, conString))
            {
                try
                {
                    conString.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var card = new Card();
                            card.ID = (int)reader["OrderID"];
                            card.Header = $"Order# {(int)reader["OrderID"]}";
                            card.Line1 = $"{(string)reader["FirstName"]} {(string)reader["LastName"]}";
                            card.Line2 = (string)reader["PhoneNumber"];
                            cards.Add(card);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                finally
                {
                    conString.Close();
                }
                return cards;
            }
        }

        public Order GetOrderDetailsByID(int id)
        {
            string query = "SELECT Order_Entries.OrderID, Products.Name, Products.Price, Products.Taxable FROM Order_Entries " +
                           "INNER JOIN Products ON Order_Entries.ProductID = Products.ProductID " +
                           "WHERE Order_Entries.OrderID = @id";
            var order = new Order();
            var products = new List<Product>();

            using (var cmd = new SqlCommand(query, conString))
            {
                try
                {
                    conString.Open();
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
                    conString.Close();
                }
            }
            order.Products = products;
            return order;

        }
    }
}
