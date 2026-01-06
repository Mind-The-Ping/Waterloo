using CSharpFunctionalExtensions;
using Waterloo.Clients.StanmoreClient;

namespace Waterloo.Integration.Tests;

public class FakeStanmoreClient : IStanmoreClient
{
    public async Task<Result<bool>> IsUserPremiumAsync(Guid userId) =>
        Result.Success(true);
}
