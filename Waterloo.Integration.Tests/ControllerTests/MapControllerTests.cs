using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Waterloo.Dtos;

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
        result.Count().Should().Be(11);
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
}
