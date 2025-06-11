using Microsoft.EntityFrameworkCore;

namespace DataAccess;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }

    public DbSet<Game> Games { get; set; }
}
