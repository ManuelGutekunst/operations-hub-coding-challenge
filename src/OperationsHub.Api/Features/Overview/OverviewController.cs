using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Features.Overview;

[ApiController]
[Route("api/overview")]
public sealed class OverviewController(InMemoryOperationsDataStore dataStore) : ControllerBase
{
    [HttpGet("metrics")]
    public IActionResult GetMetrics() =>
        Ok(dataStore.GetOverviewMetrics().Select(metric =>
            new OverviewMetricResponse(metric.Label, metric.Value, metric.MaxValue)));
}
