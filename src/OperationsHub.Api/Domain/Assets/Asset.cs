namespace OperationsHub.Api.Domain.Assets;

public sealed class Asset
{
    public required string AssetCode { get; init; }

    public required string Name { get; init; }

    public required string Category { get; init; }

    public required string Status { get; set; }
}
