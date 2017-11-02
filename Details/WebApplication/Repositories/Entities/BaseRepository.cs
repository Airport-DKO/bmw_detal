using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories.Entities
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
