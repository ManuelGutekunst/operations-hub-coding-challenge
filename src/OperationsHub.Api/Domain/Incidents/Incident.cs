namespace OperationsHub.Api.Domain.Incidents;

public sealed class Incident
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string AssetCode { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Severity { get; init; }

    public required DateTimeOffset StartsAt { get; init; }

    public DateTimeOffset? EndsAt { get; init; }

    public DateTimeOffset? PlannedEndAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
