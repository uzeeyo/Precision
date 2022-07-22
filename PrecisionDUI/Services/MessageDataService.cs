using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Precision.Model;

namespace Precision.Services
{
    public static class MessageDataService
    {
        public static ObservableCollection<Thread> LoadAllThreads()
        {
            string query = @"SELECT t.ThreadID, m.Text, m.DateSent, c.CustomerID, c.FirstName, c.LastName
                             FROM Threads t
                             INNER JOIN ( 
			                    SELECT ThreadID, MAX(DateSent) maxdate
			                    FROM Messages
			                    GROUP BY ThreadID
			                 ) AS latest
                             ON t.ThreadID = latest.ThreadID
                             INNER JOIN Messages m 
                             ON latest.ThreadID = m.ThreadID AND latest.maxdate = m.DateSent
                             LEFT JOIN Customers c
                             ON c.CustomerID = t.CustomerID
                             ORDER BY DateSent DESC";
            var threads = new ObservableCollection<Thread>();
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                try
                {
                    DataAccess.ConnectionString.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var thread = new Thread();
                        thread.LastMessage = new Message();
                        thread.Customer = new Customer();
                        thread.Id = (int)reader["ThreadID"];
                        thread.LastMessage.Text = (string)reader["Text"];
                        thread.LastMessage.Date = (DateTime)reader["DateSent"];
                        thread.Customer.CustomerID = (int)reader["CustomerID"];
                        thread.Customer.FirstName = (string)reader["FirstName"];
                        thread.Customer.LastName = (string)reader["LastName"];
                        threads.Add(thread);
                    }
                }
                catch (SqlException ex)
                {
                    DataAccess.ShowSqlErrorDialog();
                    var error = ex.Message;
                }
                finally { DataAccess.ConnectionString.Close(); }
            }
            return threads;
        }

        public static Thread LoadThreadDetails(int id)
        {
            string query = @"SELECT TOP 20 m. MessageID, m.Text, m.DateSent, m.Type, m.Origin
                             FROM Messages m
                             WHERE m.ThreadID = @id
                             ORDER BY m.DateSent ASC";
            var thread = new Thread();
            thread.Messages = new ObservableCollection<Message>();
            thread.Id = id;
            using (var cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var message = new Message();
                        message.Id = (int)reader["MessageID"];
                        message.Text = (string)reader["Text"];
                        message.Date = (DateTime)reader["DateSent"];
                        message.Type = (int)reader["Type"];
                        message.Origin = (int)reader["Origin"];
                        thread.Messages.Add(message);
                    }
                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                    DataAccess.ShowSqlErrorDialog();
                }
                finally { cmd.Connection.Close(); }
            }
            return thread;
        }
    }
}
