using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Waterloo.Database;
using Waterloo.Journey;
using Waterloo.Model;
using Waterloo.Repository.Route;

namespace Waterloo.Integration.Tests;

public class JourneyRepositoryTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    private readonly JourneyDbContext _dbContext;
    private readonly JourneyRepository _journeyRepository;

    private readonly Line _affectedLine = new(Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"), "Jubilee");
    private readonly Station _affectedStationStart = new(Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), "Bond Street");
    private readonly Station _affectedStationEnd = new(Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), "Canada Water");
    private readonly Serverity _affectedSeverity = Serverity.Severe;
    private readonly TimeOnly _affectedTime = new(8, 00);
    private readonly DayOfWeek _affectedDay = DayOfWeek.Monday;

    private readonly Model.Journey _defaultJourney;

    private readonly static TimeZoneInfo _londonTimeZone =
       TimeZoneInfo.FindSystemTimeZoneById("Europe/London");

    public JourneyRepositoryTests(CustomWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<JourneyDbContext>();

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        _journeyRepository = new JourneyRepository(_dbContext, new RouteRepository());

       _defaultJourney = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
               Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"),
               Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"),
               Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
               Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"),
               Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"),
               Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"),
               Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"),
               Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40")
              ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };
    }

    [Fact]
    public async Task JourneyRepository_Add_Journey_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        var result = await _journeyRepository.AddJourneyAsync(
            journey.UserId,
            journey.LineId,
            journey.StationIds,
            journey.StartTime,
            journey.EndTime,
            journey.DaysToCheck,
            journey.Serverity);

        result.Should().BeTrue();

        var record = _dbContext.Journeys
            .Single(x =>
            x.UserId == journey.UserId);

        record.UserId.Should().Be(journey.UserId);
        record.LineId.Should().Be(journey.LineId);
        record.StationIds.Should().BeEquivalentTo(record.StationIds);
        record.StartTime.Should().Be(ConvertToUtc(journey.StartTime));
        record.EndTime.Should().Be(ConvertToUtc(journey.EndTime));
        record.DaysToCheck.Should().BeEquivalentTo(journey.DaysToCheck);
        record.Serverity.Should().Be(journey.Serverity);
    }

    [Fact]
    public async Task JourneyRepository_Remove_Journey_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.RemoveJourneyAsync(journey.Id);
        result.Should().BeTrue();

        _dbContext.Journeys.Any(x => x.Id == journey.Id).Should().BeFalse();
    }

    [Fact]
    public async Task JourneyRepository_Remove_Journey_WrongId_Fails()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.RemoveJourneyAsync(Guid.NewGuid());
        result.Should().BeFalse();

        _dbContext.Journeys.Any(x => x.Id == journey.Id).Should().BeTrue();
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Outside_Beginning_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"),
                Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"),
                Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"),
                Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"),
                Guid.Parse("02da1648-25ed-41cf-b99b-a2eb9d448380"),
                Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"),
                Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"),
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.UserId);
        result.First().EndTime.Should().Be(journey.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Inside_Middle_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
                Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"),
                Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"),
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.UserId);
        result.First().EndTime.Should().Be(journey.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Outside_End_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
                Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"),
                Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"),
                Guid.Parse("6252902f-7fd2-45a8-a6d5-1f377e88b9be"),
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.UserId);
        result.First().EndTime.Should().Be(journey.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Full_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
                Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"),
                Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"),
                Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"),
                Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"),
                Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"),
                Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"),
                Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40")
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.UserId);
        result.First().EndTime.Should().Be(journey.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Opposite_Fails()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
                Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"),
                Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"),
                Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"),
                Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"),
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Backwards_Partial_Opposite_Successful()
    {
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"),
            StationIds = [
             Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"),
             Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"),
             Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"),
             Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433")
                ],
            StartTime = new TimeOnly(7, 00),
            EndTime = new TimeOnly(9, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedStationEnd.Id,
          _affectedStationStart.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.UserId);
        result.First().EndTime.Should().Be(journey.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_inCorrect_Line_Fails()
    {
        _defaultJourney.LineId = Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823");

        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedStationStart.Id,
          _affectedStationEnd.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData(5, 0, 7, 0)]
    [InlineData(15, 0, 17, 0)]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_inCorrect_Time_Fails(
        int startHour, 
        int startMinute, 
        int endHour, 
        int endMinute)
    {
        _defaultJourney.StartTime = new TimeOnly(startHour, startMinute);
        _defaultJourney.EndTime = new TimeOnly(endHour, endMinute);

        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
           _affectedStationStart.Id,
          _affectedStationEnd.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().BeEmpty();
    }


    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Minor_Severity_Time_Successful()
    {
        _defaultJourney.Serverity = Serverity.Minor;

        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
           _affectedStationStart.Id,
          _affectedStationEnd.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().NotBeEmpty();

        result.First().Id.Should().Be(_defaultJourney.UserId);
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Day_Tuesday_Time_Fails()
    {
        _defaultJourney.DaysToCheck = [DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday];

        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedStationEnd.Id,
          _affectedStationStart.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task JourneyRepository_GetJourneysByUserId_Successful()
    {
        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = _journeyRepository.GetJourneysByUserId(_defaultJourney.UserId);

        result.Count().Should().Be(1);
        result.Should().BeEquivalentTo([_defaultJourney]);
    }

    [Fact]
    public async Task JourneyRepository_GetJourneysByUserId_Non_Existent_User_Fails()
    {
        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = _journeyRepository.GetJourneysByUserId(Guid.NewGuid());

        result.Count().Should().Be(0);
    }

    [Fact]
    public async Task JourneyRepository_GetJourneysByUserId_Gets_Correct_Journey()
    {
        var diffJourney = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("9c834a1e-8a34-4c1e-943e-6f37b8e1e9d4"),
            StationIds = [
               Guid.Parse("8f56cc51-2827-4fcb-8983-b1959c3c1e07"),
               Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"),
               Guid.Parse("68807451-ca6b-491d-9da6-722ce632ffa6")
             ],
            StartTime = new TimeOnly(10, 00),
            EndTime = new TimeOnly(12, 00),
            DaysToCheck = [DayOfWeek.Monday, DayOfWeek.Tuesday],
            Serverity = Serverity.Minor
        };

        await _dbContext.Journeys.AddAsync(diffJourney);
        await _dbContext.Journeys.AddAsync(_defaultJourney);
        await _dbContext.SaveChangesAsync();

        var result = _journeyRepository.GetJourneysByUserId(diffJourney.UserId);

        result.Count().Should().Be(1);
        result.Should().BeEquivalentTo([diffJourney]);
    }

    private static TimeOnly ConvertToUtc(TimeOnly timeOnly)
    {
        var londonToday = TimeZoneInfo.ConvertTime(DateTime.Today, _londonTimeZone);
        var londonDateTime = londonToday.Date + timeOnly.ToTimeSpan();

        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(londonDateTime, _londonTimeZone);

        return TimeOnly.FromDateTime(utcDateTime);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
