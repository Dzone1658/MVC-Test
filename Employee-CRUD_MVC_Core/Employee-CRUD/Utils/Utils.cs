using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Utils
{
    public class Utils
    {
        public static SqlConnection GetConnection()
        {
            //ToDos: Get connection string from appsettings
            SqlConnection con = new SqlConnection("Data Source=CommonDBSP.mssql.somee.com;Initial Catalog=CommonDBSP;Persist Security Info=True;User ID=pravinvala_SQLLogin_1;Password=nrucn75l7a");
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else con.Open();

            return con;
        }
    }
}
