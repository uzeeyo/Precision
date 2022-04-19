using System.Configuration;
using System.Data.SqlClient;


namespace Precision
{
    static class DataAccessBase
    {
        static public SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);

    }
}
