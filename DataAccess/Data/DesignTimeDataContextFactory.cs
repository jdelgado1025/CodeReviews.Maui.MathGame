using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    private readonly IConfiguration _configuration;

    public DesignTimeDataContextFactory()
    {
        var basePath = Directory.GetCurrentDirectory();//AppContext.BaseDirectory;

        Console.WriteLine($"[DesignTimeDbContextFactory] Current Directory: {basePath}");

        //Build config to enable secrets and appsettings
        _configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddUserSecrets<DesignTimeDataContextFactory>()
            .Build();
    }
    public DataContext CreateDbContext(string[] args)
    {
        var conn = _configuration.GetConnectionString("DefaultConnection");

        Console.WriteLine($"[DesignTimeDbContextFactory] Connection String: {conn}");

        if (string.IsNullOrEmpty(conn))
            throw new InvalidOperationException("Could not find a connection string." +
                "Please ensure it exists in your user secrets.");

        var builder = new DbContextOptionsBuilder<DataContext>();
        
        builder.UseMySql(conn, ServerVersion.AutoDetect(conn));

        return new DataContext(builder.Options);
    }
}
