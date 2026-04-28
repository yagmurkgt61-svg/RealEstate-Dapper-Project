using Microsoft.Data.SqlClient;
using System.Data;
namespace RealEstate_Dapper_Api.Models.DapperContext
{
    public class Context
    {
        private readonly string _connectionString;

        public Context(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
            
    }
}
