using System;
using System.Configuration;
using System.Data.SqlClient;


namespace Precision.Services
{
    static class DataAccess
    {
        static private SqlConnection _connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
        static public SqlConnection ConnectionString
        {
            get { return _connectionString; }
        }


        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        public static void ShowSqlErrorDialog()
        {
            MaterialDesignThemes.Wpf.DialogHost.Show(new View.Dialogs.SqlRetrieveError());

        }

    }
}
