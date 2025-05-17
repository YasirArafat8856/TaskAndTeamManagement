using Microsoft.Data.SqlClient;
using System.Data;

namespace TaskAndTeamManagement.Infrascture.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("TaskManagementDb");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
