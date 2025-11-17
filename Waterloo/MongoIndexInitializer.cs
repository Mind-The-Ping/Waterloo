using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Waterloo.Options;

namespace Waterloo;

public class MongoIndexInitializer : IHostedService
{
    private readonly IMongoDatabase _db;
    private readonly IOptions<DatabaseOptions> _options;

    public MongoIndexInitializer(IMongoDatabase db, IOptions<DatabaseOptions> options)
    {
        _db = db;
        _options = options;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var collection = _db.GetCollection<Model.Journey>(_options.Value.Collection);

        var indexes = new List<CreateIndexModel<Model.Journey>>
        {
            new(
                Builders<Model.Journey>.IndexKeys
                    .Ascending(x => x.LineId)
                    .Ascending(x => x.StartTime)
                    .Ascending(x => x.EndTime)
                    .Ascending(x => x.Serverity)
                    .Ascending(x => x.DaysToCheck),
                new CreateIndexOptions { Name = "idx_hotpath" }
            ),
            new(
                Builders<Model.Journey>.IndexKeys
                    .Ascending(x => x.UserId),
                new CreateIndexOptions { Name = "idx_userid" }
            )
        };

        await collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
