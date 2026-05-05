using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Features.Assets;

[ApiController]
[Route("api/assets")]
public sealed class AssetsController(
    InMemoryOperationsDataStore dataStore,
    AssetBatchStatusService batchStatusService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAssets() =>
        Ok(dataStore.Assets.Select(asset => new AssetResponse(
            asset.AssetCode,
            asset.Name,
            asset.Category,
            asset.Status)));

    [HttpPatch("batch-status")]
    public IActionResult UpdateStatuses([FromBody] BatchUpdateAssetStatusesRequest request)
    {
        if (request.Updates.Count == 0)
        {
            return BadRequest(new { message = "At least one update is required." });
        }

        var result = batchStatusService.Apply(request);
        return Ok(result);
    }
}

public sealed record AssetResponse(string AssetCode, string Name, string Category, string Status);
