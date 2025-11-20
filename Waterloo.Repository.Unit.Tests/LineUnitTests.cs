using FluentAssertions;
using Waterloo.Repository.Line;

namespace Waterloo.Repository.Unit.Tests;

public class LineUnitTests
{
    private readonly LineRepository _repository = new();

    [Fact]
    public void LineRepository_GetAll_ShouldReturnCompleteListOfLines()
    {
        var result = _repository.GetAll();

        result.Should().NotBeNullOrEmpty();
        result.Count().Should().Be(16);
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

    [Fact]
    public void LineRepository_GetLinesById_Should_Return_All_Lines()
    {
        var ids = new List<Guid>()
        {
            Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"),
            Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"),
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"),
        };

        var expectedLines = new List<Model.Station>()
        {
            new(Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"), "Bakerloo"),
            new(Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"), "Central"),
            new(Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"), "Circle"),
        };

        var lines = _repository.GetLinesById(ids);

        lines.IsSuccess.Should().BeTrue();
        lines.Value.Should().BeEquivalentTo(expectedLines);
    }

    [Fact]
    public void LineRepository_GetLinesById_Should_Fail()
    {
        var ids = new List<Guid>()
        {
            Guid.Parse("8f1e8b31-d1d5-42c9-9efe-df7a391f3798"),
            Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"),
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"),
        };

        var lines = _repository.GetLinesById(ids);
        lines.IsFailure.Should().BeTrue();
        lines.Error.Should().Be($"Could not find line with id: {ids.First()}");
    }

    [Fact]
    public void LineRepository_GetLinesById_With_Same_Line_Should_Return_Line_Once()
    {
        var ids = new List<Guid>()
        {
            Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"),
            Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"),
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"),
        };

        var expectedLines = new List<Model.Station>()
        {
            new(Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"), "Bakerloo"),
            new(Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"), "Circle"),
        };

        var lines = _repository.GetLinesById(ids);

        lines.IsSuccess.Should().BeTrue();
        lines.Value.Should().BeEquivalentTo(expectedLines);
    }
}
