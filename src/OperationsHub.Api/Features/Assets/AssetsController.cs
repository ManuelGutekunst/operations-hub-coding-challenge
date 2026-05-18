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
    public IActionResult GetComponents(string assetCode)
    {
        if (!dataStore.Assets.Any(asset => asset.AssetCode.Equals(assetCode, StringComparison.OrdinalIgnoreCase)))
        {
            return NotFound(new { message = $"Unknown asset '{assetCode}'." });
        }

        var components = dataStore.AssetComponents.GetValueOrDefault(assetCode, []);

        return Ok(components.Select(component => new AssetComponentOptionResponse(component.Value, component.Label)));
    }
}

public sealed record AssetResponse(string AssetCode, string Name, string Category, string Status);
public sealed record AssetComponentOptionResponse(string Value, string Label);
