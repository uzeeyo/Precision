using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Precision.Services
{
    static class DeviceDataAccess 
    {
        public static List<Device> GetAllDevices()
        {
            var query = "SELECT * FROM Devices";
            var devices = new List<Device>();
            var conn = DataAccess.ConnectionString;
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var dev = new Device()
                        {
                            DeviceID = (int)reader["DeviceID"],
                            CustomerID = (int)reader["CustomerID"],
                            Make = (string)reader["Make"],
                            Model = (string)reader["Model"],
                            OSType = (string)reader["OSType"]
                        };
                        devices.Add(dev);
                    }
                }
                catch(Exception ex)
                {
                    var error = ex.Message;
                    System.Windows.MessageBox.Show(error);
                }
            }
            return devices;
        }
        public static List<Device> GetAllDevicesByCustomerID(Device device)
        {
            var query = "SELECT Devices.DeviceID, Devices.CustomerID, Devices.Make, Devices.Model, Devices.OSType FROM Devices" +
                        "WHERE Devices.CustomerID = @id";
            var devices = new List<Device>();
            var conn = DataAccess.ConnectionString;
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", device.CustomerID);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var dev = new Device()
                        {
                            DeviceID = (int)reader["DeviceID"],
                            CustomerID = (int)reader["CustomerID"],
                            Make = (string)reader["Make"],
                            Model = (string)reader["Model"],
                            OSType = (string)reader["OSType"]
                        };
                        devices.Add(dev);
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
            return devices;
        }

        public static void AddDevice(Device device)
        {
            
        }

        public static void RemoveDevice(int id)
        {
            string query = "DELETE FROM Devices WHERE Devices.CustomerID = @id";
            var conn = DataAccess.ConnectionString;
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    var error = ex.Message;
                    System.Windows.MessageBox.Show($"Failed to remove device: {error}");
                }
            }
        }
    }
}
