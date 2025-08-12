using System.Net.Http.Headers;
using System.Net.Http.Json;
using Waterloo.Database;
using Waterloo.Dtos;
using Waterloo.Model;

namespace Waterloo.Integration.Tests.ControllerTests;
public class JourneyControllerTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly HttpClient _client;
    private readonly Guid _id = Guid.NewGuid();
    private readonly JourneyDbContext _dbContext;

    public JourneyControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _dbContext = factory.DbContext;
        var token = factory.GenerateTestJwt(_id);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task JourneyControllers_Create_Successful()
    {
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var response = await _client.PostAsJsonAsync("api/journey/create", journeyDto);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task JourneyControllers_Delete_Successful()
    {
        var journey = new Model.Journey
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(6, 00),
            DaysToCheck = [DayOfWeek.Thursday],
            Serverity = Serverity.Severe
        };

        _dbContext.Journeys.Add(journey);
        await _dbContext.SaveChangesAsync();

        var response = await _client.DeleteAsync($"api/journey/delete?id={journey.Id}");
        response.EnsureSuccessStatusCode();
    }
}
