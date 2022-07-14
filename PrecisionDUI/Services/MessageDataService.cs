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
            string query = @"SELECT t.ThreadID, m.Text, m.DateSent
                             FROM Threads t
                             INNER JOIN ( 
			                    SELECT ThreadID, MAX(DateSent) maxdate
			                    FROM Messages
			                    GROUP BY ThreadID
			                 ) AS latest
                             ON t.ThreadID = latest.ThreadID
                             INNER JOIN Messages m 
                             ON latest.ThreadID = m.ThreadID AND latest.maxdate = m.DateSent
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
                        thread.Id = (int)reader["ThreadID"];
                        thread.LastMessage.Text = (string)reader["Text"];
                        thread.LastMessage.Date = (DateTime)reader["DateSent"];
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
    }
}
