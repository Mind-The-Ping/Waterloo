using FluentAssertions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Waterloo.Integration.Tests.ControllerTests;

public class MapControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MapControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        var token = factory.GenerateTestJwt(Guid.NewGuid());

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task MapControllers_Get_Lines_Successful()
    {
        var response = await _client.GetAsync("api/map/lines");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Line>>();
        result.Count().Should().Be(11);
    }

    [Fact]
    public async Task MapController_GetStationsByLineId_Successful()
    {
        var response = await _client.GetAsync($"api/map/stations?id={Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50")}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<Model.Station>>();

        result.Count().Should().Be(2);
        result[0].Should().NotBeEquivalentTo(new Model.Station(Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), "Waterloo"));
        result[1].Should().NotBeEquivalentTo(new Model.Station(Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), "Bank"));
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
}
