using Precision.Services;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Windows;
using System.Configuration;
using Precision.Model;

namespace Precision.Services 
{
    static class CustomerDataAccess
    {

        public static List<Customer> GetAllCustomers()
        {
            string query = "SELECT * FROM Customers WHERE ArchivedAt IS NULL";
            var customers = new List<Customer>();
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var cust = new Customer();
                            cust.CustomerID = (int)reader["CustomerID"];
                            cust.FirstName = (string)reader["FirstName"];
                            cust.LastName = (string)reader["LastName"];
                            cust.PhoneNumber = (string)reader["PhoneNumber"];
                            cust.EmailAddress = (string)reader["EmailAddress"];
                            customers.Add(cust);
                        }
                    }
                    else
                    {
                        DataAccess.ShowSqlErrorDialog();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally
                {
                    DataAccess.ConnectionString.Close();
                }
                return customers;
            }

        }

        public static Customer GetCustomerDetailsByID(int customerID)
        {
            string query = "SELECT * FROM Customers " +
                           "LEFT JOIN Addresses ON Customers.AddressID = Addresses.AddressID " +
                           "WHERE Customers.CustomerID = @id";
            var customer = new Customer();

            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();
                    cmd.Parameters.AddWithValue("@id", customerID);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.EmailAddress = DataAccess.ConvertFromDBVal<string>(reader["EmailAddress"]);
                        customer.Address.AddressID = DataAccess.ConvertFromDBVal<int?>(reader["AddressID"]);
                        if (customer.Address.AddressID != null)
                        {
                            customer.Address.Street = (string)reader["Street"];
                            customer.Address.City = (string)reader["City"];
                            customer.Address.State = (string)reader["State"];
                            customer.Address.ZipCode = (int)reader["ZipCode"];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { DataAccess.ConnectionString.Close(); }
                
            }
            return customer;
        }

        public static Customer GetCustomerByOrderID(int orderID)
        {
            string query = "SELECT Customers.CustomerID, FirstName, LastName, PhoneNumber, EmailAddress FROM Customers " +
                           "RIGHT JOIN Orders ON Orders.CustomerID = Customers.CustomerID " +
                           "WHERE Orders.OrderID = @id";
            var customer = new Customer();

            using (SqlCommand cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();
                    cmd.Parameters.AddWithValue("@id", orderID);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.EmailAddress = (string)reader["EmailAddress"];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    DataAccess.ShowSqlErrorDialog();
                }
                finally
                {
                    DataAccess.ConnectionString.Close();
                }
                
            }
            return customer;
        }


        public static void AddCustomer(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            string query = "INSERT INTO Customers VALUES (@fname, @lname, @pNumber, @eAddress)";

            using (var conn = DataAccess.ConnectionString)
            {
                try
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fname", firstName);
                        cmd.Parameters.AddWithValue("@lname", lastName);
                        cmd.Parameters.AddWithValue("@pNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@eAddress", emailAddress);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
            }

        }

        public static void EditCustomer(Customer customer)
        {
            string query = "UPDATE Customers " +
                "SET FirstName = @fname, LastName = @lname, PhoneNumber = @num, EmailAddress = @email " +
                "WHERE CustomerID = @id AND EXISTS " +
                "(" +
                "SELECT Customers.FirstName, Customers.LastName, Customers.PhoneNumber, Customers.EmailAddress " +
                "EXCEPT " +
                "SELECT @fname, @lname, @num, @email" +
                ")";
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                cmd.Parameters.AddWithValue("@id", customer.CustomerID);
                cmd.Parameters.AddWithValue("@fname", customer.FirstName);
                cmd.Parameters.AddWithValue("@lname", customer.LastName);
                cmd.Parameters.AddWithValue("@num", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", customer.EmailAddress);

                try
                {
                    DataAccess.ConnectionString.Open();
                    var affected = cmd.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    DataAccess.ShowSqlErrorDialog();
                    string error = ex.Message;
                }
                finally
                {
                    DataAccess.ConnectionString.Close();
                }
            }
        }

        public static void RemoveCustomer(int id)
        {
            string query = "UPDATE Customers SET ArchivedAt = @date WHERE CustomerID = @id";
            var date = DateTime.Now;
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    DataAccess.ConnectionString.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { DataAccess.ConnectionString.Close(); }
            }

        }
    }
}
