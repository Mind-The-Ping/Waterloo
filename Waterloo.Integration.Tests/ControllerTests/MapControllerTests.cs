using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Waterloo.Dtos;
using Waterloo.Repository.Route;

namespace Waterloo.Integration.Tests.ControllerTests;

public class MapControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly HttpClient _unauthorizedClient;

    public MapControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        var token = factory.GenerateTestJwt(Guid.NewGuid());

        _unauthorizedClient = factory.CreateClient();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task MapControllers_GetLines_Successful()
    {
        var response = await _client.GetAsync("api/map/lines");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Line>>();
        result.Count().Should().Be(13);
    }

    [Fact]
    public async Task MapController_GetLines_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync("api/map/lines");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_GetStationsByLineId_Successful()
    {
        var response = await _client.GetAsync($"api/map/stations?id={Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50")}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<Model.Station>>();

        result.Count().Should().Be(2);
        result[0].Should().BeEquivalentTo(new Model.Station(Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), "Bank"));
        result[1].Should().BeEquivalentTo(new Model.Station(Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), "Waterloo"));
    }

    [Fact]
    public async Task MapController_GetLineByName_Successful()
    {
        var response = await _client.GetAsync($"api/map/lineByName?name=bakerloo");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Model.Line>();

        result.Id.Should().Be(Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"));
        result.Name.Should().Be("Bakerloo");
    }

    [Fact]
    public async Task MapController_GetLineByName_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync($"api/map/lineByName?name=bakerloo");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_GetLineById_Successful()
    {
        var response = await _client.GetAsync($"api/map/lineById?id=9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Model.Line>();

        result.Id.Should().Be(Guid.Parse("9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6"));
        result.Name.Should().Be("Metropolitan");
    }

    [Fact]
    public async Task MapController_GetLineBy_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync($"api/map/lineById?id=9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_LinesById_Successful()
    {
        var lineIds = new List<Guid>()
        {
            Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"),
            Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"),
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"),
        };

        var expectedLines = new List<Model.Line>()
        {
            new(Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"), "Bakerloo"),
            new(Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"), "Central"),
            new(Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"), "Circle"),
        };

        var response = await _client.PostAsJsonAsync("api/map/linesById", lineIds);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Line>>();
        result.Count().Should().Be(3);
        result.Should().BeEquivalentTo(expectedLines);
    }

    [Fact]
    public async Task MapController_Lines_UnAuthorized_User_Fails()
    {
        var lineIds = new List<Guid>()
        {
            Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"),
            Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"),
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"),
        };

        var response = await _unauthorizedClient.PostAsJsonAsync("api/map/linesById", lineIds);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_GetStationsByLineId_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync($"api/map/stations?id={Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50")}");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_GetStationsByLineId_InCorrectId_Fails()
    {
        var response = await _client.GetAsync($"api/map/stations?id={Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task MapController_GetStationByName_Successful()
    {
        var response = await _client.GetAsync($"api/map/station?name=angel");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Model.Station>();

        result.Id.Should().Be(Guid.Parse("57fd1550-4c55-434e-8e96-041207c1ac63"));
        result.Name.Should().Be("Angel");
    }

    [Fact]
    public async Task MapController_GetStationByName_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync($"api/map/station?name=angel");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_GetStationByName_WrongName_Fails()
    {
        var response = await _client.GetAsync($"api/map/station?name=wrong");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task MapController_GetStationById_Successfully()
    {
        var response = await _client.GetAsync($"api/map/stationById?id=5ec78131-73b3-4b18-8e2f-d1b71eaa63d7");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Model.Station>();

        result.Id.Should().Be(Guid.Parse("5ec78131-73b3-4b18-8e2f-d1b71eaa63d7"));
        result.Name.Should().Be("Bounds Green");
    }

    [Fact]
    public async Task MapController_GetStationById_UnAuthorized_User_Fails()
    {
        var response = await _unauthorizedClient.GetAsync($"api/map/stationById?id=5ec78131-73b3-4b18-8e2f-d1b71eaa63d7");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_ToStations_Successful()
    {
        var lineId = Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50");
        var stationId = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845");

        var toStationDto = new ToStationDto(lineId, stationId);

        var response = await _client.PostAsJsonAsync("api/map/toStations", toStationDto);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Station>>();

        result.Count().Should().Be(1);
        result.ElementAt(0).Should().BeEquivalentTo(new Model.Station(Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), "Bank"));
    }

    [Fact]
    public async Task MapController_ToStations_UnAuthorized_User_Fails()
    {
        var lineId = Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50");
        var stationId = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845");

        var toStationDto = new ToStationDto(lineId, stationId);

        var response = await _unauthorizedClient.PostAsJsonAsync("api/map/toStations", toStationDto);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MapController_Stations_Successful()
    {
        var stationIds = new List<Guid>()
        {
            Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"),
            Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"),
            Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec")
        };

        var expectedStations = new List<Model.Station>()
        {
            new(Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), "Burnt Oak"),
            new(Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"), "Caledonian Road"),
            new(Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), "Camden Town")
        };

        var response = await _client.PostAsJsonAsync("api/map/stationsById", stationIds);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Station>>();
        result.Count().Should().Be(3);
        result.Should().BeEquivalentTo(expectedStations);
    }

    [Fact]
    public async Task MapController_Stations_UnAuthorized_User_Fails()
    {
        var stationIds = new List<Guid>()
        {
            Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"),
            Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"),
            Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec")
        };

        var response = await _unauthorizedClient.PostAsJsonAsync("api/map/stationsById", stationIds);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
