using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Employee_CRUD.Utils
{
    public static class Utils
    {
        public static SqlConnection GetConnection(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("GlobalMssqlConnection");
            SqlConnection con = new(connectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else con.Open();

            return con;
        }
    }
}