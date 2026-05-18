using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Features.Incidents;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Tests;

public sealed class IncidentsControllerTests
{
    [Fact]
    public void CreateIncident_rejects_unknown_assets()
    {
        var controller = new IncidentsController(new InMemoryOperationsDataStore());
        var request = new CreateIncidentRequest(
            "UNKNOWN",
            "Asset lookup failed",
            "The asset should be validated before the incident is created.",
            "Medium",
            new DateTimeOffset(2026, 05, 18, 8, 0, 0, TimeSpan.Zero),
            null,
            null);

        var result = controller.CreateIncident(request);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var message = badRequest.Value?.GetType().GetProperty("message")?.GetValue(badRequest.Value);

        Assert.Equal("Unknown asset 'UNKNOWN'.", message);
    }

    [Fact]
    public void CreateIncident_rejects_ends_at_earlier_than_starts_at()
    {
        var controller = new IncidentsController(new InMemoryOperationsDataStore());
        var request = new CreateIncidentRequest(
            "WT-1001",
            "Bad end date",
            "The end time cannot be before the start time.",
            "High",
            new DateTimeOffset(2026, 05, 18, 10, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2026, 05, 18, 9, 0, 0, TimeSpan.Zero),
            null);

        var result = controller.CreateIncident(request);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var message = badRequest.Value?.GetType().GetProperty("message")?.GetValue(badRequest.Value);

        Assert.Equal("endsAt must not be earlier than startsAt.", message);
    }
}
