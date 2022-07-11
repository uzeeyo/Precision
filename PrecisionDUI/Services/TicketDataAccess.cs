using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Precision.Model;
using System.Data.SqlClient;

namespace Precision.Services
{
    public class TicketDataAccess
    {
        public static List<Ticket> GetAllTicketsByCustomer(int id)
        {
            List<Ticket> tickets = new List<Ticket>();
            string query = "SELECT t.TicketID, t.CreationDate, t.Description, d.DeviceID, d.Make, d.Model " +
                           "FROM Tickets t " +
                           "LEFT JOIN Ticket_Device_Entries tde ON tde.TicketID = t.TicketID " +
                           "LEFT JOIN Devices d ON d.DeviceID = tde.DeviceID " +
                           "WHERE t.CustomerID = @id";

            using (SqlCommand cmd = new SqlCommand(query, DataAccess.ConnectionString))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    DataAccess.ConnectionString.Open();
                    int currentTicketID = -1;
                    var reader = cmd.ExecuteReader();
                    var ticket = new Ticket();
                    ticket.TicketID = -1;


                    while (reader.Read())
                    {
                        if (currentTicketID == (int)reader["TicketID"] || currentTicketID == -1)
                        {
                            ticket.TicketID = (int)reader["TicketID"];
                            if (reader["DeviceID"] != DBNull.Value)
                            {
                                var device = new Device();
                                device.Model = (string)reader["Model"];
                                ticket.Description = (string)reader["Description"].ToString();
                                ticket.Devices.Add(device);
                            }

                            currentTicketID = (int)reader["TicketID"];
                        }
                        else
                        {
                            tickets.Add(ticket);
                            ticket = new Ticket();

                            ticket.TicketID = (int)reader["TicketID"];
                            if (reader["DeviceID"] != DBNull.Value)
                            {
                                var device = new Device();
                                device.Model = (string)reader["Model"];
                                ticket.Description = DataAccess.ConvertFromDBVal<string>(reader["Description"]);
                                ticket.Devices.Add(device);
                            }
                            currentTicketID = (int)reader["TicketID"];
                        }
                    }
                    if (ticket.TicketID != -1)
                    {
                        tickets.Add(ticket);
                    }
                    

                    

                }
                catch (SqlException ex)
                {
                    var error = ex.Message;
                }
                finally
                {
                    DataAccess.ConnectionString.Close();
                }
            }

            return tickets;
        }


    }
}
