using OperationsHub.Api.Domain.Assets;
using OperationsHub.Api.Domain.Incidents;

namespace OperationsHub.Api.Infrastructure;

public sealed class InMemoryOperationsDataStore
{
    public List<Asset> Assets { get; } =
    [
        new() { AssetCode = "WT-1001", Name = "Wind Turbine 1001", Category = "Turbine", Status = "Running" },
        new() { AssetCode = "WT-1002", Name = "Wind Turbine 1002", Category = "Turbine", Status = "Maintenance" },
        new() { AssetCode = "SB-2001", Name = "Substation 2001", Category = "Substation", Status = "Running" },
        new() { AssetCode = "PK-3001", Name = "North Park", Category = "Park", Status = "Attention" }
    ];

    public List<Incident> Incidents { get; } =
    [
        new()
        {
            AssetCode = "WT-1002",
            Title = "Gearbox follow-up",
            Description = "Maintenance window still under review.",
            Severity = "Medium",
            StartsAt = DateTimeOffset.UtcNow.AddDays(-1),
            PlannedEndAt = DateTimeOffset.UtcNow.AddDays(1)
        }
    ];

    public Dictionary<string, IReadOnlyList<AssetComponentOption>> AssetComponents { get; } =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["WT-1001"] =
            [
                new("rotor", "Rotor"),
                new("yaw-drive", "Yaw drive"),
                new("converter", "Converter")
            ],
            ["WT-1002"] =
            [
                new("gearbox", "Gearbox"),
                new("generator", "Generator"),
                new("hydraulics", "Hydraulics")
            ],
            ["SB-2001"] =
            [
                new("transformer", "Transformer"),
                new("protection-relay", "Protection relay"),
                new("switchgear", "Switchgear")
            ],
            ["PK-3001"] =
            [
                new("lighting", "Lighting"),
                new("cctv", "CCTV"),
                new("access-gate", "Access gate")
            ]
        };
}
