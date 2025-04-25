using Bankify.Interfaces;
using Microsoft.Data.SqlClient;

namespace Bankify
{
    public class BankifyContext : IBankifyContext 
    {
        private readonly string _connString;

        public BankifyContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("BankAppData");
        }

        // 🔹 NY KONSTRUKTOR för tester
        public BankifyContext(string testConnectionString)
        {
            _connString = testConnectionString;
        }


        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
