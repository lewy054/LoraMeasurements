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

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Measurement>>> GetMeasurements(string id, CancellationToken cancellationToken)
    {
        var measurements = await _context.Measurements.Where(e => e.DeviceId == id).ToListAsync(cancellationToken);
        return Ok(measurements);
    }
}