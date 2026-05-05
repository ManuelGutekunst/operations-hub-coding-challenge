using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Features.Assets;

public sealed class AssetBatchStatusService(InMemoryOperationsDataStore dataStore)
{
    private static readonly HashSet<string> AllowedStatuses =
    [
        "Running",
        "Attention",
        "Maintenance",
        "Offline"
    ];

    public BatchUpdateAssetStatusesResponse Apply(BatchUpdateAssetStatusesRequest request)
    {
        var updated = new List<string>();
        var failed = new List<AssetBatchUpdateFailure>();

        foreach (var item in request.Updates)
        {
            if (string.IsNullOrWhiteSpace(item.AssetCode))
            {
                failed.Add(new AssetBatchUpdateFailure(item.AssetCode, "Asset code is required."));
                continue;
            }

            if (!AllowedStatuses.Contains(item.Status))
            {
                failed.Add(new AssetBatchUpdateFailure(item.AssetCode, $"Status '{item.Status}' is not supported."));
                continue;
            }

            var asset = dataStore.Assets.FirstOrDefault(candidate =>
                candidate.AssetCode.Equals(item.AssetCode, StringComparison.OrdinalIgnoreCase));

            if (asset is null)
            {
                failed.Add(new AssetBatchUpdateFailure(item.AssetCode, "Asset not found."));
                continue;
            }

            asset.Status = item.Status;
            updated.Add(asset.AssetCode);
        }

        return new BatchUpdateAssetStatusesResponse(updated, failed);
    }
}
