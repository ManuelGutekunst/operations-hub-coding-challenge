using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Features.Assets;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Tests;

public sealed class AssetsControllerTests
{
    [Fact]
    public void GetComponents_returns_seeded_components_for_the_selected_asset()
    {
        var controller = new AssetsController(new InMemoryOperationsDataStore());

        var result = controller.GetComponents("WT-1002");

        var ok = Assert.IsType<OkObjectResult>(result);
        var components = Assert.IsAssignableFrom<IEnumerable<AssetComponentOptionResponse>>(ok.Value).ToArray();

        Assert.Collection(
            components,
            component =>
            {
                Assert.Equal("gearbox", component.Value);
                Assert.Equal("Gearbox", component.Label);
            },
            component =>
            {
                Assert.Equal("generator", component.Value);
                Assert.Equal("Generator", component.Label);
            },
            component =>
            {
                Assert.Equal("hydraulics", component.Value);
                Assert.Equal("Hydraulics", component.Label);
            });
    }

    [Fact]
    public void GetComponents_matches_asset_codes_case_insensitively()
    {
        var controller = new AssetsController(new InMemoryOperationsDataStore());

        var result = controller.GetComponents("sb-2001");

        var ok = Assert.IsType<OkObjectResult>(result);
        var components = Assert.IsAssignableFrom<IEnumerable<AssetComponentOptionResponse>>(ok.Value).ToArray();

        Assert.Equal(3, components.Length);
        Assert.Equal("transformer", components[0].Value);
    }

    [Fact]
    public void GetComponents_returns_not_found_for_unknown_assets()
    {
        var controller = new AssetsController(new InMemoryOperationsDataStore());

        var result = controller.GetComponents("UNKNOWN");

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var message = notFound.Value?.GetType().GetProperty("message")?.GetValue(notFound.Value);

        Assert.Equal("Unknown asset 'UNKNOWN'.", message);
    }
}
