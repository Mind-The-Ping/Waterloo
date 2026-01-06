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

    private readonly IEnumerable<Disruption> _disruptions = [];

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

        _disruptions = [new Disruption(
            Guid.NewGuid(),
            _affectedStationStart.Id,
            _affectedStationEnd.Id,
            _affectedSeverity)];
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
            _affectedTime,
            _affectedDay,
            _disruptions);

        result.Should().NotBeEmpty();
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().DisruptionId.Should().Be(_disruptions.First().Id);
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
            _affectedTime,
            _affectedDay,
            _disruptions);

        result.Should().NotBeEmpty();
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(_disruptions.First().Id);
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
            _affectedTime,
            _affectedDay,
            _disruptions);

        result.Should().NotBeEmpty();
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(_disruptions.First().Id);
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
            _affectedTime,
            _affectedDay,
            _disruptions);

        result.Should().NotBeEmpty();
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(_disruptions.First().Id);
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
            _affectedTime,
            _affectedDay,
            _disruptions);

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

        var disruptions = new List<Disruption>()
        {
            new(
            Guid.NewGuid(),
            _affectedStationEnd.Id,
            _affectedStationStart.Id,
            _affectedSeverity)
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedTime,
          _affectedDay,
          disruptions);

        result.Should().NotBeEmpty();
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
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
          _affectedTime,
          _affectedDay,
          _disruptions);

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
          _affectedTime,
          _affectedDay,
          _disruptions);

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
          _affectedTime,
          _affectedDay,
          _disruptions);

        result.Should().NotBeEmpty();

        result.First().JourneyId.Should().Be(_defaultJourney.Id);
        result.First().UserId.Should().Be(_defaultJourney.UserId);
        result.First().DisruptionId.Should().Be(_disruptions.First().Id);
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
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Severe_No_Overlap_2_Results()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee

        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
                Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
                Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
                Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), // West Hampstead
                Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), // Finchley Road
                Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"), // Swiss Cottage
                Guid.Parse("02da1648-25ed-41cf-b99b-a2eb9d448380"), // St. John's Wood
                Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), // Baker Street
                Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), // Bond Street
                Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), // Green Park
                Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), // Westminster
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), // Waterloo
                Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"), // Southwark
                Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), // London Bridge
                Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"), // Bermondsey
                Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), // Canada Water
                Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), // Canary Wharf
                Guid.Parse("6252902f-7fd2-45a8-a6d5-1f377e88b9be"), // North Greenwich
                Guid.Parse("752cd9c1-bead-404f-b12a-aa93c212f2c2"), // Canning Town
                Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), // West Ham
                Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), // Stratford
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), // Waterloo
                Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), // Stratford
                Serverity.Severe),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          lineId,
          _affectedTime,
          _affectedDay,
          disruptions);

        result.Count().Should().Be(2);

        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(8);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));

        result.Last().JourneyId.Should().Be(journey.Id);
        result.Last().UserId.Should().Be(journey.UserId);
        result.Last().DisruptionId.Should().Be(disruptions.Last().Id);
        result.Last().StartStation.Id.Should().Be(journey.StationIds.First());
        result.Last().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.Last().EndTime.Should().Be(_defaultJourney.EndTime);
        result.Last().AffectedStations.Should().HaveCount(10);
        result.Last().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(17)));
        result.Last().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(18)));
        result.Last().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(19)));
        result.Last().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(20)));
        result.Last().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(21)));
        result.Last().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(22)));
        result.Last().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(23)));
        result.Last().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(24)));
        result.Last().AffectedStations.ElementAt(8).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(25)));
        result.Last().AffectedStations.ElementAt(9).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(26)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Severe_Closed_No_Overlap_2_Notifications()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
                Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
                Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
                Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), // West Hampstead
                Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), // Finchley Road
                Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"), // Swiss Cottage
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Serverity.Closed),

            new(
                Guid.NewGuid(),
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
                Serverity.Severe),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
         lineId,
         _affectedTime,
         _affectedDay,
         disruptions);

        result.Count().Should().Be(2);

        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(5);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));

        result.Last().JourneyId.Should().Be(journey.Id);
        result.Last().UserId.Should().Be(journey.UserId);
        result.Last().DisruptionId.Should().Be(disruptions.Last().Id);
        result.Last().StartStation.Id.Should().Be(journey.StationIds.First());
        result.Last().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.Last().EndTime.Should().Be(_defaultJourney.EndTime);
        result.Last().AffectedStations.Should().HaveCount(3);
        result.Last().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.Last().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));
        result.Last().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(8)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Severe_Closed_Closed_Two_Notifications()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
               Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
               Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
               Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
               Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
               Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
               Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
               Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
               Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
               Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
               Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), // West Hampstead
               Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), // Finchley Road
               Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"), // Swiss Cottage
               Guid.Parse("02da1648-25ed-41cf-b99b-a2eb9d448380"), // St.John's Wood
               Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), // Baker Street
                ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), // Stratford
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
                Serverity.Closed),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          lineId,
          _affectedTime,
          _affectedDay,
          disruptions);

        result.Count().Should().Be(2);

        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.Last().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(8);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));

        result.Last().JourneyId.Should().Be(journey.Id);
        result.Last().UserId.Should().Be(journey.UserId);
        result.Last().DisruptionId.Should().Be(disruptions.First().Id);
        result.Last().StartStation.Id.Should().Be(journey.StationIds.First());
        result.Last().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.Last().EndTime.Should().Be(_defaultJourney.EndTime);
        result.Last().AffectedStations.Should().HaveCount(6);
        result.Last().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(8)));
        result.Last().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(9)));
        result.Last().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(10)));
        result.Last().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(11)));
        result.Last().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(12)));
        result.Last().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(13)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Severe_Closed_Two_Notifications()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
                Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
                Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), // Willesden Green
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
                Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), // West Hampstead
                Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), // Finchley Road
                Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"), // Swiss Cottage
                Guid.Parse("02da1648-25ed-41cf-b99b-a2eb9d448380"), // St. John's Wood
                Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), // Baker Street
                Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), // Bond Street
                Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), // Green Park
                Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), // Westminster
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), // Waterloo
                Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"), // Southwark
                Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), // London Bridge
                Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"), // Bermondsey
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), // Stratford
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), // Kilburn
                Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), // Westminster
                Serverity.Closed),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            lineId,
            _affectedTime,
            _affectedDay,
            disruptions);

        result.Count().Should().Be(2);

        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.Last().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(9);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(8)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(9)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(10)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(11)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(12)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(13)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(14)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(15)));
        result.First().AffectedStations.ElementAt(8).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(16)));

        result.Last().JourneyId.Should().Be(journey.Id);
        result.Last().UserId.Should().Be(journey.UserId);
        result.Last().DisruptionId.Should().Be(disruptions.First().Id);
        result.Last().StartStation.Id.Should().Be(journey.StationIds.First());
        result.Last().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.Last().EndTime.Should().Be(_defaultJourney.EndTime);
        result.Last().AffectedStations.Should().HaveCount(12);
        result.Last().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.Last().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.Last().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.Last().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.Last().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.Last().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.Last().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.Last().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));
        result.Last().AffectedStations.ElementAt(8).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(17)));
        result.Last().AffectedStations.ElementAt(9).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(18)));
        result.Last().AffectedStations.ElementAt(10).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(19)));
        result.Last().AffectedStations.ElementAt(11).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(20)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Opposite_Direction_Same_Severity_1_Notification()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
                Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
                Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Serverity.Severe),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            lineId,
            _affectedTime,
            _affectedDay,
            disruptions);

        result.Count().Should().Be(1);
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(7);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Opposite_Different_Severity_Direction_1_Notification()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"); // Jubilee
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), // Canons Park
                Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), // Queensbury
                Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), // Kingsbury
                Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), // Wembley Park
                Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), // Neasden
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), // Dollis Hill
                Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), // Stanmore
                Serverity.Closed),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            lineId,
            _affectedTime,
            _affectedDay,
            disruptions);

        result.Count().Should().Be(1);
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(7);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Branching_Correct_Disruption_With_Same_Stations()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("62e93d5d-cc67-4c42-8ff5-24582f89d624"); // Northern
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), // South Wimbledon
                Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), // Colliers Wood
                Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), // Tooting Broadway
                Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), // Tooting Bec
                Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), // Balham
                Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), // Clapham Common
                Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), // Clapham North
                Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), // Stockwell
                Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), // Oval
                Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), // Kennington
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), // Euston
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), // Warren Street
                Serverity.Severe),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
           lineId,
           _affectedTime,
           _affectedDay,
           disruptions);

        result.Count().Should().Be(1);
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.First().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(11);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));
        result.First().AffectedStations.ElementAt(8).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(8)));
        result.First().AffectedStations.ElementAt(9).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(9)));
        result.First().AffectedStations.ElementAt(10).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(10)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Branching_Correct_Disruption_With_Different_Stations()
    {
        await InitializeAsync();

        var lineId = Guid.Parse("62e93d5d-cc67-4c42-8ff5-24582f89d624"); // Northern
        var journey = new Model.Journey()
        {
            UserId = Guid.NewGuid(),
            LineId = lineId,
            StationIds = [
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), // South Wimbledon
                Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), // Colliers Wood
                Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), // Tooting Broadway
                Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), // Tooting Bec
                Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), // Balham
                Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), // Clapham Common
                Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), // Clapham North
                Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), // Stockwell
                Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), // Oval
                Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), // Kennington
                Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), // Waterloo
                Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), // Embankment
                Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), // Charing Cross
            ],
            StartTime = ConvertToUtc(_defaultStartTime),
            EndTime = ConvertToUtc(_defaultEndTime),
            DaysToCheck = [DayOfWeek.Monday],
            Serverity = Serverity.Severe,
            CreatedAt = DateTime.UtcNow,
        };

        await _journeyCollection.InsertOneAsync(journey);

        var disruptions = new List<Disruption>()
        {
            new(
                Guid.NewGuid(),
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), // Euston
                Serverity.Severe),

            new(
                Guid.NewGuid(),
                Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), // Morden
                Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), // Warren Street
                Serverity.Severe),
        };

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
           lineId,
           _affectedTime,
           _affectedDay,
           disruptions);

        result.Count().Should().Be(1);
        result.First().JourneyId.Should().Be(journey.Id);
        result.First().UserId.Should().Be(journey.UserId);
        result.First().DisruptionId.Should().Be(disruptions.Last().Id);
        result.First().StartStation.Id.Should().Be(journey.StationIds.First());
        result.First().EndStation.Id.Should().Be(journey.StationIds.Last());
        result.First().EndTime.Should().Be(_defaultJourney.EndTime);
        result.First().AffectedStations.Should().HaveCount(14);
        result.First().AffectedStations.ElementAt(0).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(0)));
        result.First().AffectedStations.ElementAt(1).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(1)));
        result.First().AffectedStations.ElementAt(2).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(2)));
        result.First().AffectedStations.ElementAt(3).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(3)));
        result.First().AffectedStations.ElementAt(4).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(4)));
        result.First().AffectedStations.ElementAt(5).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(5)));
        result.First().AffectedStations.ElementAt(6).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(6)));
        result.First().AffectedStations.ElementAt(7).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(7)));
        result.First().AffectedStations.ElementAt(8).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(8)));
        result.First().AffectedStations.ElementAt(9).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(9)));
        result.First().AffectedStations.ElementAt(10).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(10)));
        result.First().AffectedStations.ElementAt(11).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(11)));
        result.First().AffectedStations.ElementAt(12).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(12)));
        result.First().AffectedStations.ElementAt(13).Should().Be(_stationRepository.GetStationById(journey.StationIds.ElementAt(13)));
    }

    [Fact]
    public async Task JourneyRepository_GetUserIdsForAffectedJourneysAsync_Day_Tuesday_Time_Fails()
    {
        await InitializeAsync();

        _defaultJourney.DaysToCheck = [DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday];

        await _journeyCollection.InsertOneAsync(_defaultJourney);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
          _affectedLine.Id,
          _affectedTime,
          _affectedDay,
          _disruptions);

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

    [Fact]
    public async Task JourneyRepository_GetUserJourneyCountAsync_Journey_Added_Successful()
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
            Serverity = Serverity.Severe,
           
        };

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserJourneyCountAsync(journey.UserId);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);
    }

    [Fact]
    public async Task JourneyRepository_GetUserJourneyCountAsync_No_Journey_Added_Successful()
    {
        await InitializeAsync();

        var result = await _journeyRepository.GetUserJourneyCountAsync(Guid.NewGuid());

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(0);
    }

    [Fact]
    public async Task JourneyRepository_GetUserJourneyCountAsync_Journey_Added_Deleted_Successful()
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
            Serverity = Serverity.Severe,
            DeletedAt = DateTime.UtcNow
        };

        await _journeyCollection.InsertOneAsync(journey);

        var result = await _journeyRepository.GetUserJourneyCountAsync(journey.UserId);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(0);
    }

    private static TimeOnly ConvertToUtc(TimeOnly timeOnly)
    {
        var londonToday = TimeZoneInfo.ConvertTime(DateTime.Today, _londonTimeZone);
        var londonDateTime = londonToday.Date + timeOnly.ToTimeSpan();

        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(londonDateTime, _londonTimeZone);

        return TimeOnly.FromDateTime(utcDateTime);
    }
}
