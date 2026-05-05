namespace OperationsHub.Api.Features.Incidents;

public sealed record CreateIncidentRequest(
    string AssetCode,
    string Title,
    string Description,
    string Severity,
    DateTimeOffset StartsAt,
    DateTimeOffset? EndsAt,
    DateTimeOffset? PlannedEndAt);
