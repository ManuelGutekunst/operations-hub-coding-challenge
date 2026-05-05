using OperationsHub.Api.Features.Assets;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Tests;

public sealed class AssetBatchStatusServiceTests
{
    [Fact]
    public void Apply_updates_known_assets()
    {
        var store = new InMemoryOperationsDataStore();
        var service = new AssetBatchStatusService(store);

        var response = service.Apply(new BatchUpdateAssetStatusesRequest(
            [
                new AssetStatusUpdateItem("WT-1001", "Offline")
            ]));

        Assert.Single(response.UpdatedAssetCodes);
        Assert.Empty(response.Failed);
        Assert.Equal("Offline", store.Assets.Single(asset => asset.AssetCode == "WT-1001").Status);
    }

    [Fact]
    public void Apply_reports_unknown_assets_as_failures()
    {
        var store = new InMemoryOperationsDataStore();
        var service = new AssetBatchStatusService(store);

        var response = service.Apply(new BatchUpdateAssetStatusesRequest(
            [
                new AssetStatusUpdateItem("UNKNOWN", "Running")
            ]));

        Assert.Empty(response.UpdatedAssetCodes);
        Assert.Single(response.Failed);
        Assert.Equal("Asset not found.", response.Failed.Single().Reason);
    }
}
