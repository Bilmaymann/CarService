using System.Data.SqlClient;
using System.Data;

namespace CarService.Repositories
{
    public class BaseRepository
    {
        private readonly IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}