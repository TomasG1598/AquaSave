using System.Configuration;
using System.Data.SqlClient;

namespace AquaSave.Data
{
    
    public sealed class DbConnection
    {
        private static DbConnection _instance;
        private readonly string _connectionString;

        private DbConnection()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["AquaSaveDb"]
                .ConnectionString;
        }

        public static DbConnection GetInstance()
        {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}