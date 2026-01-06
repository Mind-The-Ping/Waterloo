using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Claims;
using System.Text;
using Waterloo.Clients.StanmoreClient;

namespace Waterloo.Integration.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public readonly string DatabaseName = $"testdb_{Guid.NewGuid():N}";
    public IMongoDatabase Database { get; private set; } = null!;
    public IConfiguration Configuration { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var mongoDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDatabase));
            if (mongoDescriptor != null) {
                services.Remove(mongoDescriptor);
            }

            var client = new MongoClient("mongodb://localhost:27017");
            Database = client.GetDatabase(DatabaseName);

            services.AddSingleton(Database);

            var stanmoreDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(IStanmoreClient));

            if (stanmoreDescriptor != null) {
                services.Remove(stanmoreDescriptor);
            }

            services.AddSingleton<IStanmoreClient>(
                 new FakeStanmoreClient());

            var sp = services.BuildServiceProvider();
            Configuration = sp.GetRequiredService<IConfiguration>();
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await Database.Client.DropDatabaseAsync(DatabaseName);
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
            Issuer = "stratford",
            Audience = "waterloo"
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }
}
