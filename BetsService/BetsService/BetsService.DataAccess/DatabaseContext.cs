using Microsoft.EntityFrameworkCore;
using BetsService.Domain;

namespace BetsService.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
