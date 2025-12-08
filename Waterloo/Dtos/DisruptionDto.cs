using Waterloo.Model;

namespace Waterloo.Dtos;

public record DisruptionDto(
    Guid Id,
    Guid StartStationId,
    Guid EndStationId,
    Serverity Serverity);