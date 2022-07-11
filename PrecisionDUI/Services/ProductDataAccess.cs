using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using Precision.Model;

namespace Precision.Services
{
    static class ProductDataAccess
    {
        public static ObservableCollection<Product> GetAllProductsNames()
        {
            string query = "SELECT * FROM Products";
            var products = new ObservableCollection<Product>();

            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.ProductID = (int)reader["ProductID"];
                        product.Name = (string)reader["Name"];
                        products.Add(product);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    DataAccess.ConnectionString.Close();
                }
            }
            return products;
        }

        public static ObservableCollection<Product> GetSearchedProducts(string param)
        {
            string query = "SELECT TOP 50 ProductID, Name FROM Products WHERE Name LIKE @p";
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                var products = new ObservableCollection<Product>();
                try
                 {
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@p", "%" + param + "%");
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.ProductID = (int)reader["ProductID"];
                        product.Name = (string)reader["Name"];
                        products.Add(product);
                    }
                }
                catch(SqlException ex)
                {
                    var error = ex.ToString();
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { cmd.Connection.Close(); }
                return products;

            }
        }
    }
}
