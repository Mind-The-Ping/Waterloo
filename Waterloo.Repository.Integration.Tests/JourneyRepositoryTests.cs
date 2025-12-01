using FluentAssertions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NSubstitute;
using Waterloo.Model;
using Waterloo.Options;
using Waterloo.Repository.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;
using Waterloo.Repository.Station;

namespace Waterloo.Repository.Integration.Tests;

public class JourneyRepositoryTests
{
    private readonly JourneyRepository _journeyRepository;
    private readonly LineRepository _lineRepository;
    private readonly StationRepository _stationRepository;


    private readonly Model.Line _affectedLine = new(Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"), "Jubilee");
    private readonly Model.Station _affectedStationStart = new(Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), "Bond Street");
    private readonly Model.Station _affectedStationEnd = new(Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), "Canada Water");
    private readonly Serverity _affectedSeverity = Serverity.Severe;
    private readonly TimeOnly _affectedTime = new(8, 00);
    private readonly DayOfWeek _affectedDay = DayOfWeek.Monday;
    private readonly ILogger<JourneyRepository> _logger = Substitute.For<ILogger<JourneyRepository>>();

    private readonly TimeOnly _defaultStartTime = new TimeOnly(7, 00);
    private readonly TimeOnly _defaultEndTime = new TimeOnly(9, 00);
    private readonly Model.Journey _defaultJourney;

    private readonly static TimeZoneInfo _londonTimeZone =
       TimeZoneInfo.FindSystemTimeZoneById("Europe/London");

    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<Model.Journey> _journeyCollection;

    private readonly string _databaseName = $"testdb_{Guid.NewGuid():N}";

    public JourneyRepositoryTests()
    {
        var client = new MongoClient("mongodb://localhost:27017");

        _mongoDatabase = client.GetDatabase(_databaseName);
        _lineRepository = new LineRepository();
        _stationRepository = new StationRepository();

        var databaseOptions = new DatabaseOptions()
        {
            Name = "Waterloo",
            Collection = "Journeys",
            ConnectionString = "mongodb://localhost:27017"
        };

        var options = Microsoft.Extensions.Options.Options.Create(databaseOptions);

        _journeyRepository = new JourneyRepository(
            options,
            _mongoDatabase,
            _lineRepository,
            new RouteRepository(), 
            _stationRepository,
            _logger);

        _journeyCollection = _mongoDatabase.GetCollection<Model.Journey>(databaseOptions.Collection);

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
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };
    }

    private async Task InitializeAsync()
    {
        await _mongoDatabase.Client.DropDatabaseAsync(_databaseName);
    }

    [Fact]
    public async Task JourneyRepository_Add_Journey_Successful()
    {
        await InitializeAsync();

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

        result.IsSuccess.Should().BeTrue();

        var record = await _journeyCollection
            .Find(x => x.UserId == journey.UserId)
            .SingleOrDefaultAsync();

        record.UserId.Should().Be(journey.UserId);
        record.LineId.Should().Be(journey.LineId);
        record.StationIds.Should().BeEquivalentTo(record.StationIds);
        record.StartTime.Should().Be(ConvertToUtc(journey.StartTime));
        record.EndTime.Should().Be(ConvertToUtc(journey.EndTime));
        record.DaysToCheck.Should().BeEquivalentTo(journey.DaysToCheck);
        record.Serverity.Should().Be(journey.Serverity);
        record.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task JourneyRepository_Remove_Journey_Successful()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.RemoveJourneyAsync(journey.Id);
        result.IsSuccess.Should().BeTrue();

        var deletedUser = await _journeyCollection.Find(x => x.Id == journey.Id).FirstOrDefaultAsync();

        deletedUser.Should().NotBeNull();
        deletedUser.UserId.Should().Be(journey.UserId);
        deletedUser.LineId.Should().Be(journey.LineId);
        deletedUser.StationIds.Should().BeEquivalentTo(journey.StationIds);
        deletedUser.StartTime.Should().Be(journey.StartTime);
        deletedUser.EndTime.Should().Be(journey.EndTime);
        deletedUser.DaysToCheck.Should().BeEquivalentTo(journey.DaysToCheck);
        deletedUser.Serverity.Should().Be(journey.Serverity);
        deletedUser.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(30));
    }

    [Fact]
    public async Task JourneyRepository_Remove_Journey_WrongId_Fails()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.RemoveJourneyAsync(Guid.NewGuid());
        result.IsSuccess.Should().BeTrue();

        _journeyCollection.Find(x => x.Id == journey.Id).Any().Should().BeTrue();
    }

    [Fact]
    public async Task JourneyRepository_Remove_Journey_UserId_Successful()
    {
        await InitializeAsync();

        var journey1 = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        var journey2 = new Model.Journey()
        {
            UserId = journey1.UserId,
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Wednesday],
            Serverity = Serverity.Minor
        };

        var journey3 = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            StationIds = [Guid.NewGuid()],
            StartTime = new TimeOnly(5, 00),
            EndTime = new TimeOnly(8, 00),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe
        };

        await _journeyCollection.InsertManyAsync(
            [journey1, journey2, journey3]);

        var deletedAt = DateTime.UtcNow;
        var result = await _journeyRepository.RemoveJourneyByUserIdAsync(journey1.UserId, deletedAt);

        var deletedUsers = await _journeyCollection.Find(x => x.UserId == journey1.UserId).ToListAsync();
        deletedUsers.Count.Should().Be(2);

        deletedUsers.First().UserId.Should().Be(journey1.UserId);
        deletedUsers.First().LineId.Should().Be(journey1.LineId);
        deletedUsers.First().StationIds.Should().BeEquivalentTo(journey1.StationIds);
        deletedUsers.First().StartTime.Should().Be(journey1.StartTime);
        deletedUsers.First().EndTime.Should().Be(journey1.EndTime);
        deletedUsers.First().DaysToCheck.Should().BeEquivalentTo(journey1.DaysToCheck);
        deletedUsers.First().Serverity.Should().Be(journey1.Serverity);
        deletedUsers.First().DeletedAt.Should().BeCloseTo(deletedAt, TimeSpan.FromSeconds(30));

        deletedUsers.Last().UserId.Should().Be(journey2.UserId);
        deletedUsers.Last().LineId.Should().Be(journey2.LineId);
        deletedUsers.Last().StationIds.Should().BeEquivalentTo(journey2.StationIds);
        deletedUsers.Last().StartTime.Should().Be(journey2.StartTime);
        deletedUsers.Last().EndTime.Should().Be(journey2.EndTime);
        deletedUsers.Last().DaysToCheck.Should().BeEquivalentTo(journey2.DaysToCheck);
        deletedUsers.Last().Serverity.Should().Be(journey2.Serverity);
        deletedUsers.Last().DeletedAt.Should().BeCloseTo(deletedAt, TimeSpan.FromSeconds(30));

        var leftAloneUser = await _journeyCollection.Find(x => x.UserId == journey3.UserId).FirstOrDefaultAsync();

        leftAloneUser.Should().NotBeNull();
        leftAloneUser.UserId.Should().Be(journey3.UserId);
        leftAloneUser.LineId.Should().Be(journey3.LineId);
        leftAloneUser.StationIds.Should().BeEquivalentTo(journey3.StationIds);
        leftAloneUser.StartTime.Should().Be(journey3.StartTime);
        leftAloneUser.EndTime.Should().Be(journey3.EndTime);
        leftAloneUser.DaysToCheck.Should().BeEquivalentTo(journey3.DaysToCheck);
        leftAloneUser.Serverity.Should().Be(journey3.Serverity);
        leftAloneUser.DeletedAt.Should().BeNull();
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Outside_Beginning_Successful()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().StartStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.First()));
        result.First().EndStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.Last()));
        result.First().EndTime.Should().Be(journey.EndTime);
        result.First().AffectedStations.Should().HaveCount(1);
        result.First().AffectedStations.First().Should().Be(_stationRepository.GetStationById(journey.StationIds.Last()));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Inside_Middle_Successful()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().StartStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.First()));
        result.First().EndStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.Last()));
        result.First().EndTime.Should().Be(journey.EndTime);
        result.First().AffectedStations.Should().HaveCount(3);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Outside_End_Successful()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().StartStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.First()));
        result.First().EndStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.Last()));
        result.First().EndTime.Should().Be(journey.EndTime);
        result.First().AffectedStations.Should().HaveCount(1);
        result.First().AffectedStations.First().Should().Be(_stationRepository.GetStationById(journey.StationIds.First()));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Full_Successful()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            _affectedLine.Id,
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity,
            _affectedTime,
            _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().StartStation.Should().Be(_affectedStationStart);
        result.First().EndStation.Should().Be(_affectedStationEnd);
        result.First().EndTime.Should().Be(journey.EndTime);
        result.First().AffectedStations.Should().HaveCount(8);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Foward_Partial_Opposite_Fails()
    {
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

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
        await InitializeAsync();

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

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedStationEnd.Id,
          _affectedStationStart.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().NotBeEmpty();
        result.First().Id.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().StartStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.First()));
        result.First().EndStation.Should().Be(_stationRepository.GetStationById(journey.StationIds.Last()));
        result.First().EndTime.Should().Be(journey.EndTime);
        result.First().AffectedStations.Should().HaveCount(4);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_inCorrect_Line_Fails()
    {
        await InitializeAsync();

        _defaultJourney.LineId = Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823");

        await _journeyCollection.InsertOneAsync(_defaultJourney);

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
        await InitializeAsync();

        _defaultJourney.StartTime = new TimeOnly(startHour, startMinute);
        _defaultJourney.EndTime = new TimeOnly(endHour, endMinute);

        await _journeyCollection.InsertOneAsync(_defaultJourney);

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
        await InitializeAsync();

        _defaultJourney.Serverity = Serverity.Minor;

        await _journeyCollection.InsertOneAsync(_defaultJourney);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedStationStart.Id,
          _affectedStationEnd.Id,
          _affectedSeverity,
          _affectedTime,
          _affectedDay);

        result.Should().NotBeEmpty();

        result.First().Id.Should().Be(_defaultJourney.Id);
        result.First().UserId.Should().Be(_defaultJourney.UserId);
        result.First().StartStation.Should().Be(_affectedStationStart);
        result.First().EndStation.Should().Be(_affectedStationEnd);
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(8);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(_defaultJourney.StationIds.ElementAt(7)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Day_Tuesday_Time_Fails()
    {
        await InitializeAsync();

        _defaultJourney.DaysToCheck = [DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday];

        await _journeyCollection.InsertOneAsync(_defaultJourney);

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
    public async Task JourneyRepository_GetJourneysByUserIdAsync_Successful()
    {
        await InitializeAsync();

        await _journeyCollection.InsertOneAsync(_defaultJourney);

        var result = await _journeyRepository.GetJourneysByUserIdAsync(_defaultJourney.UserId);

        result.Count().Should().Be(1);
        result.Should().BeEquivalentTo([
            new JourneyReturn(
                _defaultJourney.Id,
                _lineRepository.GetLineById(_defaultJourney.LineId)!,
                _stationRepository.GetStationById(_defaultJourney.StationIds.First())!,
                _stationRepository.GetStationById(_defaultJourney.StationIds.Last())!,
                _defaultStartTime,
                _defaultEndTime,
                _defaultJourney.DaysToCheck,
                _defaultJourney.Serverity)]);
    }

    [Fact]
    public async Task JourneyRepository_GetJourneysByUserIdAsync_Non_Existent_User_Fails()
    {
        await InitializeAsync();

        await _journeyCollection.InsertOneAsync(_defaultJourney);

        var result = await _journeyRepository.GetJourneysByUserIdAsync(Guid.NewGuid());

        result.Count().Should().Be(0);
    }

    [Fact]
    public async Task JourneyRepository_GetJourneysByUserId_Gets_Correct_Journey()
    {
        await InitializeAsync();

        var startTime = new TimeOnly(10, 00);
        var endTime = new TimeOnly(12, 00);

        var diffJourney = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = Guid.Parse("9c834a1e-8a34-4c1e-943e-6f37b8e1e9d4"),
            StationIds = [
               Guid.Parse("8f56cc51-2827-4fcb-8983-b1959c3c1e07"),
               Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"),
               Guid.Parse("68807451-ca6b-491d-9da6-722ce632ffa6")
             ],
            StartTime = ConvertToUtc(startTime),
            EndTime = ConvertToUtc(endTime),
            DaysToCheck = [DayOfWeek.Monday, DayOfWeek.Tuesday],
            Serverity = Serverity.Minor,
            CreatedAt = DateTime.UtcNow
        };

        await _journeyCollection.InsertOneAsync(diffJourney);
        await _journeyCollection.InsertOneAsync(_defaultJourney);

        var result = await _journeyRepository.GetJourneysByUserIdAsync(diffJourney.UserId);

        result.Count().Should().Be(1);
        result.Should().BeEquivalentTo([
          new JourneyReturn(
                diffJourney.Id,
                _lineRepository.GetLineById(diffJourney.LineId)!,
                _stationRepository.GetStationById(diffJourney.StationIds.First())!,
                _stationRepository.GetStationById(diffJourney.StationIds.Last())!,
                startTime,
                endTime,
                diffJourney.DaysToCheck,
                diffJourney.Serverity)]);
    }

    private static TimeOnly ConvertToUtc(TimeOnly timeOnly)
    {
        var londonToday = TimeZoneInfo.ConvertTime(DateTime.Today, _londonTimeZone);
        var londonDateTime = londonToday.Date + timeOnly.ToTimeSpan();

        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(londonDateTime, _londonTimeZone);

        return TimeOnly.FromDateTime(utcDateTime);
    }
}
