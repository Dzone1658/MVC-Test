using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Data;
using System.IO;

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

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}