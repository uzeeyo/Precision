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
            string query = "SELECT * FROM Customers";
            var customers = new List<Customer>();
            using (var cmd = new SqlCommand(query, DataAccessBase.conString))
            {
                try
                {
                    DataAccessBase.conString.Open();
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
                        MessageBox.Show("No data was found.");
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
                finally
                {
                    DataAccessBase.conString.Close();
                }
                return customers;
            }

        }

        public static Customer GetCustomerByID(int customerID)
        {
            string query = "SELECT * FROM Customers WHERE CustomerID = @id";
            var customer = new Customer();

            using (var cmd = new SqlCommand(query, DataAccessBase.conString))
            {
                cmd.Parameters.AddWithValue("@id", customerID);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer.CustomerID = (int)reader["ID"];
                    customer.FirstName = (string)reader["FirstName"];
                    customer.LastName = (string)reader["LastName"];
                    customer.PhoneNumber = (string)reader["PhoneNumber"];
                    customer.EmailAddress = (string)reader["EmailAddress"];
                }
            }
            return customer;
        }

        public static Customer GetCustomerByOrderID(int orderID)
        {
            string query = "SELECT Customers.CustomerID, FirstName, LastName, PhoneNumber, EmailAddress FROM Customers " +
                           "RIGHT JOIN Orders ON Orders.CustomerID = Customers.CustomerID " +
                           "WHERE Orders.OrderID = @id";
            var customer = new Customer();

            using (SqlCommand cmd = new SqlCommand(query, DataAccessBase.conString))
            {
                try
                {
                    DataAccessBase.conString.Open();
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
                }
                finally
                {
                    DataAccessBase.conString.Close();
                }
                
            }
            return customer;
        }


        public static void AddCustomer(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            string query = "INSERT INTO Customers VALUES (@fname, @lname, @pNumber, @eAddress)";

            using (var conn = DataAccessBase.conString)
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
                }
            }

        }

        public static void RemoveCustomer(int id)
        {
            string query = "DELETE FROM Customers WHERE CustomerID = @id";
            using (var conn = DataAccessBase.conString)
            {
                try
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }

        }
    }
}
