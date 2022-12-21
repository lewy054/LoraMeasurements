using LoraMeasurementsBackend.DAL;
using LoraMeasurementsBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoraMeasurementsBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly ILogger<TTNController> _logger;
    private readonly ApplicationDbContext _context;

    public MeasurementController(ILogger<TTNController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}/{from}/{to}")]
    public async Task<ActionResult<List<Measurement>>> GetMeasurements(string id, long from, long to, CancellationToken cancellationToken)
    {
        var fromDate = DateTimeOffset.FromUnixTimeSeconds(from / 1000).UtcDateTime;
        var toDate = DateTimeOffset.FromUnixTimeSeconds(to / 1000).UtcDateTime;
        var t =  _context.Measurements
            .Where(e => e.DeviceId == id).OrderBy(e=> e.MeasurementTime).ToList();
        var measurements = await _context.Measurements
            .Where(e => e.DeviceId == id)
            .OrderBy(e=> e.MeasurementTime)
            .Where(obj => obj.MeasurementTime >= fromDate && obj.MeasurementTime < toDate)
            .ToListAsync(cancellationToken);
        return Ok(measurements);
    }
}