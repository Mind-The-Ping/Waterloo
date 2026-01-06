using CSharpFunctionalExtensions;

namespace Waterloo.Clients.StanmoreClient;

public interface IStanmoreClient
{
    public Task<Result<bool>> IsUserPremiumAsync(Guid userId);
}
