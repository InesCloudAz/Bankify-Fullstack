using Microsoft.Data.SqlClient;
using System.Data;

namespace Bankify.Interfaces
{
    public interface IBankifyContext
    {
        public SqlConnection GetConnection();



    }
}
