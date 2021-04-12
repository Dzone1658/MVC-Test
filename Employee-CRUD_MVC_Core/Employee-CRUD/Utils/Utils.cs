using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Utils
{
    public static class Utils
    {
        public static SqlConnection GetConnection(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("GlobalMssqlConnection");
            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else con.Open();

            return con;
        }
    }
}