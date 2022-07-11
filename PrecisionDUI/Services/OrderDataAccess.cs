using System;
using System.Collections.Generic;
using Precision.Model;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;

namespace Precision.Services
{
    static class OrderDataAccess
    {
        //TODO: Figure out what to include in Order Cards. Possibly add Name and other info to Order models
        public static List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            string query = "SELECT Orders.OrderID, Orders.CreatedAt, Orders.CustomerID, Customers.FirstName, Customers.LastName " +
                "FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID " +
                "WHERE Orders.ArchivedAt IS NULL " +
                "ORDER BY Orders.OrderID DESC";

            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var order = new Order();
                        order.OrderID = (int)reader["OrderID"];
                        order.Customer.CustomerID = (int)reader["CustomerID"];
                        order.Customer.FirstName = (string)reader["FirstName"];
                        order.Customer.LastName = (string)reader["LastName"];
                        order.CreatedAt = (DateTime)reader["CreatedAt"];

                        orders.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MaterialDesignThemes.Wpf.DialogHost.Show(new View.Dialogs.SqlRetrieveError());
                }
                finally { cmd.Connection.Close(); }
            }
            return orders;
        }

        public static Order GetOrderDetailsByID(int id)
        {
            string query = "SELECT Order_Entries.OrderID, Products.Name, Products.ProductID, Order_Entries.EntryID, Order_Entries.PriceChange, " +
                           "(CASE WHEN TaxableChange IS NOT NULL " +
                                "THEN TaxableChange " +
                                "ELSE Products.Taxable " +
                                "END) AS Taxable, " +
                           "(CASE WHEN Order_Entries.PriceChange IS NOT NULL " +
                                "THEN Order_Entries.PriceChange " +
                                "ELSE Products.Price " +
                                "END) AS Price " +
                           "FROM Order_Entries " +
                           "LEFT JOIN Products ON Order_Entries.ProductID = Products.ProductID " +
                           "WHERE Order_Entries.OrderID = @id";
            var order = new Order();
            order.OrderID = id;
            var conn = DataAccess.ConnectionString;


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
                        product.ProductID = (int)reader["ProductID"];
                        product.EntryID = (int)reader["EntryID"];
                        product.Name = (string)reader["Name"];
                        product.Price = (decimal)reader["Price"];
                        product.Taxable = reader.GetBoolean(reader.GetOrdinal("Taxable"));
                        order.Products.Add(product);

                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MaterialDesignThemes.Wpf.DialogHost.Show(new View.Dialogs.SqlRetrieveError());
                }
                finally
                {
                    conn.Close();
                }
            }
            return order;

        }

        public static void AddProductToOrderID(int orderID, int productID)
        {
            string query = "INSERT INTO Order_Entries (Order_Entries.OrderID, Order_Entries.ProductID) VALUES (@oid, @pid)";

            using (var cmd = new SqlCommand(query))
            {
                try
                {
                    cmd.Connection = DataAccess.ConnectionString;
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@oid", orderID);
                    cmd.Parameters.AddWithValue("@pid", productID);
                    var affected = cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                    MaterialDesignThemes.Wpf.DialogHost.Show(new View.Dialogs.SqlRetrieveError());
                }
                finally { cmd.Connection.Close(); }
            }
        }

        public static void EditProductPrice(int entryID, decimal newPrice)
        {
            string query = "UPDATE Order_Entries SET PriceChange = @price WHERE EntryID = @id";
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@price", newPrice);
                    cmd.Parameters.AddWithValue("@id", entryID);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { cmd.Connection.Close(); }
            }
        }

        public static void ChangeProductTaxable(int entryID, bool taxable)
        {
            string query = "UPDATE Order_Entries SET TaxableChange = @taxable WHERE EntryID = @entryID";
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@taxable", taxable);
                    cmd.Parameters.AddWithValue("@entryID", entryID);
                    var affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }

        public static void DeleteOrderByID(int orderID)
        {
            string query = "UPDATE Orders SET ArchivedAt = @date WHERE OrderID = @id";
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", orderID);
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    var error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { cmd.Connection.Close(); }
            }
        }

        public static void RemoveProductFromOrderID(int entryID)
        {
            string query = "DELETE FROM Order_Entries WHERE Order_Entries.EntryID = @eid";

            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@eid", entryID);
                    DataAccess.ConnectionString.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    var error = ex.Message.ToString();
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { cmd.Connection.Close(); }
            }
        }

    }
}
