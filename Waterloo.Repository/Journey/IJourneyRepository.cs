using CSharpFunctionalExtensions;
using Waterloo.Model;

namespace Waterloo.Repository.Journey;

public interface IJourneyRepository
{
    public Task<Result> AddJourneyAsync(
        Guid userId, 
        Guid lineId, 
        IEnumerable<Guid> stationIds,
        TimeOnly startTime,
        TimeOnly endTime,
        IEnumerable<DayOfWeek> daysToCheck,
        Serverity serverity);

    public Task<Result> RemoveJourneyAsync(Guid id);

    public Task<Result> RemoveJourneyByUserIdAsync(Guid userId, DateTime deletedAt);

    public Task<IEnumerable<AffectedUserDto>> GetUserIdsForAffectedJourneysAsync(
        Guid line,
        TimeOnly dateTime,
        DayOfWeek queryDay,
        IEnumerable<Disruption> disruptions);

    public Task<IEnumerable<JourneyReturn>> GetJourneysByUserIdAsync(Guid userId);

    public Task<Result<int>> GetUserJourneyCountAsync(Guid userId);

    public Task<Result> RemovePremiumJourneysAsync(Guid userId);
}
