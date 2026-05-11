using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Features.Assets;

[ApiController]
[Route("api/assets")]
public sealed class AssetsController(InMemoryOperationsDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAssets() =>
        Ok(dataStore.Assets.Select(asset => new AssetResponse(
            asset.AssetCode,
            asset.Name,
            asset.Category,
            asset.Status)));
}

public sealed record AssetResponse(string AssetCode, string Name, string Category, string Status);
