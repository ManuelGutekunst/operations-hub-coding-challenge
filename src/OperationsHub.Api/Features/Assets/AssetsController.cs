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

    [HttpGet("{assetCode}/components")]
    public IActionResult GetAssetComponents(string assetCode)
    {
        if (!dataStore.AssetComponents.TryGetValue(assetCode, out var components))
        {
            return NotFound(new { message = $"Unknown asset '{assetCode}'." });
        }

        return Ok(components.Select(component => new AssetComponentOptionResponse(
            component.Value,
            component.Label)));
    }
}

public sealed record AssetResponse(string AssetCode, string Name, string Category, string Status);
public sealed record AssetComponentOptionResponse(string Value, string Label);
