namespace Waterloo.Consumer;

public record UserDeleted(
    Guid UserId,
    DateTime DeletedAt);
