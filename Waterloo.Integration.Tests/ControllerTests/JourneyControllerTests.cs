using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Waterloo.Database;
using Waterloo.Dtos;
using Waterloo.Model;

namespace Waterloo.Integration.Tests.ControllerTests;
public class JourneyControllerTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly HttpClient _client;
    private readonly HttpClient _unauthorizedClient;
    private readonly Guid _id = Guid.NewGuid();
    private readonly JourneyDbContext _dbContext;

    public JourneyControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        var token = factory.GenerateTestJwt(_id);
        _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

        _unauthorizedClient = factory.CreateClient();

        _dbContext = factory.DbContext;
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
    public async Task JourneyControllers_Create_UnAuthorized_User_Fails()
    {
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var response = await _unauthorizedClient.PostAsJsonAsync("api/journey/create", journeyDto);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JourneyControllers_Create_WrongLineId_Fails()
    {
        var journeyDto = new JourneyDto(
            Guid.NewGuid(),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var response = await _client.PostAsJsonAsync("api/journey/create", journeyDto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task JourneyControllers_Create_Wrong_StartStationId_Fails()
    {
        var journeyDto = new JourneyDto(
           Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
           Guid.NewGuid(),
           Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
           new TimeOnly(10, 00),
           new TimeOnly(13, 00),
           [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
           Serverity.Minor);

        var response = await _client.PostAsJsonAsync("api/journey/create", journeyDto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task JourneyControllers_Create_Wrong_EndStationId_Fails()
    {
        var journeyDto = new JourneyDto(
           Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
           Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
           Guid.NewGuid(),
           new TimeOnly(10, 00),
           new TimeOnly(13, 00),
           [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
           Serverity.Minor);

        var response = await _client.PostAsJsonAsync("api/journey/create", journeyDto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task JourneyControllers_Create_Wrong_StartAndEndStationId_Fails()
    {
        var journeyDto = new JourneyDto(
           Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
           Guid.NewGuid(),
           Guid.NewGuid(),
           new TimeOnly(10, 00),
           new TimeOnly(13, 00),
           [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
           Serverity.Minor);

        var response = await _client.PostAsJsonAsync("api/journey/create", journeyDto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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

    [Fact]
    public async Task JourneyControllers_Delete_UnAuthorized_User_Fails()
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

        var response = await _unauthorizedClient.DeleteAsync($"api/journey/delete?id={journey.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JourneyControllers_Delete_WrongId_Fails()
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

        var response = await _client.DeleteAsync($"api/journey/delete?id={Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task JourneyControllers_Get_AffectedJourneys_Successful()
    {
        var journey = new Model.Journey
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), 
                          Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717")],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(6, 00),
            DaysToCheck = [DayOfWeek.Thursday],
            Serverity = Serverity.Severe
        };

        _dbContext.Journeys.Add(journey);
        await _dbContext.SaveChangesAsync();

        var url = QueryHelpers.AddQueryString(
            "api/journey/affectedJourneys",
            new Dictionary<string, string?>
            {
                ["LineId"] = journey.LineId.ToString(),
                ["StartStationId"] = "5059c39d-2492-49a0-9eaa-0a2c6cdfa605",
                ["EndStationId"] = "3686c2bf-12bd-43cf-8975-6891896189ba",
                ["QueryTime"] = new TimeOnly(5, 30).ToString("HH:mm"),
                ["QueryDay"] = journey.DaysToCheck.First().ToString(),
                ["Serverity"] = Serverity.Suspended.ToString()
            });

        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<Guid>>();
        result.First().Should().Be(journey.UserId);
    }

    [Fact]
    public async Task JourneyControllers_Get_AffectedJourneys_UnAuthorized_User_Fails()
    {
        var journey = new Model.Journey
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"),
                          Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717")],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(6, 00),
            DaysToCheck = [DayOfWeek.Thursday],
            Serverity = Serverity.Severe
        };

        _dbContext.Journeys.Add(journey);
        await _dbContext.SaveChangesAsync();

        var url = QueryHelpers.AddQueryString(
            "api/journey/affectedJourneys",
            new Dictionary<string, string?>
            {
                ["LineId"] = journey.LineId.ToString(),
                ["StartStationId"] = "5059c39d-2492-49a0-9eaa-0a2c6cdfa605",
                ["EndStationId"] = "3686c2bf-12bd-43cf-8975-6891896189ba",
                ["QueryTime"] = new TimeOnly(5, 30).ToString("HH:mm"),
                ["QueryDay"] = journey.DaysToCheck.First().ToString(),
                ["Serverity"] = Serverity.Suspended.ToString()
            });

        var response = await _unauthorizedClient.GetAsync(url);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}