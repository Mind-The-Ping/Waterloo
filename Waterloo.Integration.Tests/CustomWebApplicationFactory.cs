using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Waterloo.Database;

namespace Waterloo.Integration.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"testdb_{Guid.NewGuid():N}";
    public JourneyDbContext DbContext { get; private set; } = null!;
    public IConfiguration Configuration { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<JourneyDbContext>));

            if (descriptor != null) {
                services.Remove(descriptor);
            }

            services.AddDbContext<JourneyDbContext>(options => {
                options.UseNpgsql($"Host=localhost;Port=5432;Database={_databaseName};Username=waterlooUser;Password=password12345");
            });

            var sp = services.BuildServiceProvider();

            Configuration = sp.GetRequiredService<IConfiguration>();

            var db = sp.GetRequiredService<JourneyDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            DbContext = db;
        });
    }

    public string GenerateTestJwt(Guid userId) =>
        CreateToken(userId, Configuration);


    private static string CreateToken(Guid userId, IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            ([
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
             ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration.GetSection("Jwt:Issuers").Get<string[]>()!.First(),
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }
}
