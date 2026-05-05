namespace OperationsHub.Api.Features.Incidents;

public sealed record IncidentResponse(
    Guid Id,
    string AssetCode,
    string Title,
    string Description,
    string Severity,
    DateTimeOffset StartsAt,
    DateTimeOffset? EndsAt,
    DateTimeOffset? PlannedEndAt,
    DateTimeOffset CreatedAt);
