using Waterloo.Model;

namespace Waterloo.Journey;

public interface IJourneyRepository
{
    public Task<bool> AddJourneyAsync(
        Guid userId, 
        Guid lineId, 
        IEnumerable<Guid> stationIds,
        TimeOnly startTime,
        TimeOnly endTime,
        IEnumerable<DayOfWeek> daysToCheck,
        Serverity serverity);

    public Task<bool> RemoveJourneyAsync(Guid id);

    public Task<IEnumerable<AffectedUser>> GetUserIdsForAffectedJourneysAsync(
        Guid line,
        Guid startStation,
        Guid endStation,
        Serverity serverity,
        TimeOnly dateTime,
        DayOfWeek queryDay);

    public IEnumerable<JourneyReturn> GetJourneysByUserId(Guid userId);
}
