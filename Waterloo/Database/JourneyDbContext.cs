using Microsoft.EntityFrameworkCore;

namespace Waterloo.Database;

public class JourneyDbContext : DbContext
{
    public DbSet<Model.Journey> Journeys { get; set; }

    public JourneyDbContext(DbContextOptions<JourneyDbContext> options)
      : base(options) { }
}
