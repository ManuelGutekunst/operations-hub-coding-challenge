using Microsoft.AspNetCore.Mvc;
using OperationsHub.Api.Domain.Incidents;
using OperationsHub.Api.Infrastructure;

namespace OperationsHub.Api.Features.Incidents;

[Route("api/incidents")]
public sealed class IncidentsController(InMemoryOperationsDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public IActionResult GetIncidents() =>
        Ok(dataStore.Incidents
            .OrderByDescending(incident => incident.CreatedAt)
            .Select(MapResponse));

    [HttpPost]
    public IActionResult CreateIncident([FromBody] CreateIncidentRequest request)
    {
        var incident = new Incident
        {
            AssetCode = request.AssetCode,
            Title = request.Title,
            Description = request.Description,
            Severity = request.Severity,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt,
            PlannedEndAt = request.PlannedEndAt
        };

        dataStore.Incidents.Add(incident);

        return CreatedAtAction(nameof(GetIncidents), new { id = incident.Id }, MapResponse(incident));
    }

    private static IncidentResponse MapResponse(Incident incident) =>
        new(
            incident.Id,
            incident.AssetCode,
            incident.Title,
            incident.Description,
            incident.Severity,
            incident.StartsAt,
            incident.EndsAt,
            incident.PlannedEndAt,
            incident.CreatedAt);
}
