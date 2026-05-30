using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kas_kelas__2_.Config
{
    public class Database
    {
        private string connectionString =
            "Data Source=DESKTOP-8GGN937;" +    
            "Initial Catalog=dbkaskelas;" +
            "Integrated Security=True";

        public SqlConnection GetConnection()
        {
            Debug.WriteLine("Database.GetConnection() -> " + connectionString);
            return new SqlConnection(connectionString);
        }
    }
}