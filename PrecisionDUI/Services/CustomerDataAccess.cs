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
    class CustomerDataAccess : DataAccessBase
    {
        
        public List<Customer> GetAllCustomers()
        {
            string query = "SELECT * FROM Customers";
            var customers = new List<Customer>();
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
                    conString.Close();
                }
                return customers;
            }

        }



        public void AddCustomer(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            string query = "INSERT INTO Customers VALUES (@fname, @lname, @pNumber, @eAddress)";

            using (var conn = conString)
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

        public void RemoveCustomer(int id)
        {
            string query = "DELETE FROM Customers WHERE CustomerID = @id";
            using (conString)
            {
                try
                {
                    using (var cmd = new SqlCommand(query, conString))
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
