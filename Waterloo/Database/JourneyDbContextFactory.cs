using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Waterloo.Database;

public class JourneyDbContextFactory : IDesignTimeDbContextFactory<JourneyDbContext>
{
    public JourneyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .AddJsonFile("appsettings.Development.json", optional: true)
             .Build();

        var optionsBuilder = new DbContextOptionsBuilder<JourneyDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

        return new JourneyDbContext(optionsBuilder.Options);
    }
}
