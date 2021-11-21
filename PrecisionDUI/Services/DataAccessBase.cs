using System.Configuration;
using System.Data.SqlClient;


namespace Precision
{
    class DataAccessBase
    {
        public SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);

    }
}
