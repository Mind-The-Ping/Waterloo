using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NSubstitute;
using Waterloo.Clients.StanmoreClient;
using Waterloo.Dtos;
using Waterloo.Model;
using Waterloo.Model.Options;
using Waterloo.Options;
using Waterloo.Repository.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;
using Waterloo.Repository.Station;

namespace Waterloo.Integration.Tests;

public class JourneyOrchestratorTests
{
    private readonly JourneyOrchestrator _journeyOrchestrator;
    private readonly IJourneyRepository _journeyRepository;
    private readonly IStanmoreClient _stanmoreClient;

    private readonly string _databaseName = $"testdb_{Guid.NewGuid():N}";

    private readonly IMongoDatabase _mongoDatabase;

    public JourneyOrchestratorTests()
    {
        var lineRepository = new LineRepository();
        var routeRepository = new RouteRepository();
        var stationRepository = new StationRepository();

        var client = new MongoClient("mongodb://localhost:27017");

        _mongoDatabase = client.GetDatabase(_databaseName);

        var databaseOptions = new DatabaseOptions()
        {
            Name = "Waterloo",
            Collection = "Journeys",
            ConnectionString = "mongodb://localhost:27017"
        };

        var options = Microsoft.Extensions.Options.Options.Create(databaseOptions);

        var repositoryLogger = Substitute.For<ILogger<JourneyRepository>>();

        _journeyRepository = new JourneyRepository(
           options,
           _mongoDatabase,
           lineRepository,
           routeRepository,
           stationRepository,
           repositoryLogger);


        _stanmoreClient = Substitute.For<IStanmoreClient>();
        var journeyOpt = new JourneyOptions()
        {
            FreeSaveLimit = 1,
            PremiumSaveLimit = 10,
        };

        var journeyOptions = Microsoft.Extensions.Options.Options.Create(journeyOpt);
        var orchestratorLogger = Substitute.For<ILogger<JourneyOrchestrator>>();

        _journeyOrchestrator = new JourneyOrchestrator(
            lineRepository,
            routeRepository,
            _journeyRepository,
            _stanmoreClient,
            journeyOptions,
            orchestratorLogger);
    }

    private async Task InitializeAsync() {
        await _mongoDatabase.Client.DropDatabaseAsync(_databaseName);
    }


    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_InCorrectLineId_Fails()
    {
        var userId = Guid.NewGuid();
        var journeyDto = new JourneyDto(
            Guid.NewGuid(),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var result = await _journeyOrchestrator.CreateJourneyAsync(userId, journeyDto);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Line Id is Invalid {journeyDto.LineId}.");
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_InCorrectStartStationId_Fails()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(false));

        var userId = Guid.NewGuid();
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.NewGuid(),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var result = await _journeyOrchestrator.CreateJourneyAsync(userId, journeyDto);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Either your start station id is invalid {journeyDto.StartStationId} " +
                          $"or end station id is {journeyDto.EndStationId}.");
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_InCorrectEndStationId_Fails()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(false));

        var userId = Guid.NewGuid();
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.NewGuid(),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var result = await _journeyOrchestrator.CreateJourneyAsync(userId, journeyDto);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Either your start station id is invalid {journeyDto.StartStationId} " +
                          $"or end station id is {journeyDto.EndStationId}.");
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_InCorrectStartAndEndStationId_Fails()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(false));

        var userId = Guid.NewGuid();
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.NewGuid(),
            Guid.NewGuid(),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var result = await _journeyOrchestrator.CreateJourneyAsync(userId, journeyDto);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Either your start station id is invalid {journeyDto.StartStationId} " +
                          $"or end station id is {journeyDto.EndStationId}.");
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_Free_Successful()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(false));

        var userId = Guid.NewGuid();
        var journeyDto = new JourneyDto(
            Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
            Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
            Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
            new TimeOnly(10, 00),
            new TimeOnly(13, 00),
            [DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Saturday],
            Serverity.Minor);

        var result = await _journeyOrchestrator.CreateJourneyAsync(userId, journeyDto);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_Free_Second_Journey_Fails()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(false));

        var userId = Guid.NewGuid();
        var journeys = CreateMany(2);

        var firstResult = await _journeyOrchestrator.CreateJourneyAsync(userId, journeys.First());
        firstResult.IsSuccess.Should().BeTrue();

        var secondResult = await _journeyOrchestrator.CreateJourneyAsync(userId, journeys.Last());
        secondResult.IsFailure.Should().BeTrue();
        secondResult.Error.Should().Be("Free users can only save one journey. Upgrade to Premium to save more.");
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_Premium_Successful()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(true));

        var userId = Guid.NewGuid();
        var journeys = CreateMany(2);

        foreach (var journey in journeys)
        {
            var result = await _journeyOrchestrator
                .CreateJourneyAsync(userId, journey);

            result.IsSuccess.Should().BeTrue();
        }
    }

    [Fact]
    public async Task JourneyOrchestrator_CreateJourneyAsync_Premium_Tenth_Journey_Fails()
    {
        await InitializeAsync();

        _stanmoreClient.IsUserPremiumAsync(Arg.Any<Guid>())
            .Returns(Result.Success(true));

        var userId = Guid.NewGuid();
        var journeys = CreateMany(11);

        for (int i = 0; i < 10; i++)
        {
            var result = await _journeyOrchestrator
                .CreateJourneyAsync(userId, journeys.ElementAt(i));

            result.IsSuccess.Should().BeTrue();
        }

        var tenthResult = await _journeyOrchestrator.CreateJourneyAsync(userId, journeys.Last());
        tenthResult.IsFailure.Should().BeTrue();
        tenthResult.Error.Should().Be("You have reached the maximum number of saved journeys for your subscription.");
    }

    public static IEnumerable<JourneyDto> CreateMany(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new JourneyDto(
                LineId: Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"),
                StartStationId: Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"),
                EndStationId: Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"),
                StartTime: new TimeOnly(8, 0),
                EndTime: new TimeOnly(9, 0),
                DaysToCheck: [DayOfWeek.Monday],
                Serverity: Serverity.Minor
            );
        }
    }
}
