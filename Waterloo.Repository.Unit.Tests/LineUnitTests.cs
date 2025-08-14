using FluentAssertions;
using Waterloo.Repository.Line;
using Waterloo.Repository.Station;

namespace Waterloo.Repository.Unit.Tests;

public class LineUnitTests
{
    private readonly LineRepository _repository = new();

    [Fact]
    public void LineRepository_GetAll_ShouldReturnCompleteListOfLines()
    {
        var result = _repository.GetAll();

        result.Should().NotBeNullOrEmpty();
        result.Count().Should().Be(11);
    }

    [Fact]
    public void LineRepository_GetLineById_Should_Return_Line() 
    {
        var id = Guid.Parse("9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6");
        var result = _repository.GetLineById(id);

        result.Id.Should().Be(id);
        result.Name.Should().Be("Metropolitan");
    }

    [Theory]
    [InlineData("Waterloo & City")]
    [InlineData("Waterloo&City")]
    [InlineData("waTerLoo & cItY")]
    public void StationRepository_GetStationByName_CorrectStation(string name)
    {
        var result = _repository.GetLineByName(name);

        result.Name.Should().Be("Waterloo & City");
        result.Id.Should().Be(Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50"));
    }
}
