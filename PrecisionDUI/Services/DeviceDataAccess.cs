using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Precision.Services
{
    class DeviceDataAccess : DataAccessBase
    {
        public List<Device> GetAllDevices()
        {
            var query = "SELECT * FROM Devices";
            var devices = new List<Device>();
            using (var cmd = new SqlCommand(query, conString))
            {
                try
                {
                    conString.Open();
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
        public List<Device> GetAllDevicesByCustomerID(Device device)
        {
            var query = "SELECT Devices.DeviceID, Devices.CustomerID, Devices.Make, Devices.Model, Devices.OSType FROM Devices" +
                        "WHERE Devices.CustomerID = @id";
            var devices = new List<Device>();
            using (var cmd = new SqlCommand(query, conString))
            {
                try
                {
                    conString.Open();
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
                    conString.Close();
                }
            }
            return devices;
        }

        public void AddDevice(Device device)
        {
            
        }

        public void RemoveDevice(int id)
        {
            string query = "DELETE FROM Devices WHERE Devices.CustomerID = @id";
            using(var cmd = new SqlCommand(query, conString))
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
