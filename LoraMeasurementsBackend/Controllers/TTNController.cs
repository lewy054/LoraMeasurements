using System.Globalization;
using LoraMeasurementsBackend.DAL;
using LoraMeasurementsBackend.Dtos;
using LoraMeasurementsBackend.Dtos.PayLoad;
using LoraMeasurementsBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoraMeasurementsBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class TTNController : ControllerBase
{
    private readonly ILogger<TTNController> _logger;
    private readonly ApplicationDbContext _context;

    public TTNController(ILogger<TTNController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Data request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("post");
        var id = request.EndDeviceIds.DeviceId;
        var device = await _context.Devices.Where(e => e.Id == id).Include(e => e.Measurements)
            .FirstOrDefaultAsync(cancellationToken);
        var location = request.UplinkMessage.RxMetadata[0].Location;
        if (device == null)
        {
            device = new Device()
            {
                Id = id,
                Location = new Model.Location(location.Latitude.ToString(CultureInfo.InvariantCulture),
                    location.Longitude.ToString(CultureInfo.InvariantCulture)),
                ApplicationId = request.EndDeviceIds.ApplicationIds.ApplicationId,
            };
            device.Measurements = new List<Measurement>()
            {
                new()
                {
                    DeviceId = device.Id,
                    Device = device,
                    Temperature = request.UplinkMessage.DecodedPayload.Temperature,
                    BarometricPressure = request.UplinkMessage.DecodedPayload.BarometricPressure,
                    RelativeHumidity = request.UplinkMessage.DecodedPayload.RelativeHumidity,
                    AnalogIn = request.UplinkMessage.DecodedPayload.AnalogIn,
                    MeasurementTime = Convert.ToDateTime(request.UplinkMessage.RxMetadata[0].Time)
                }
            };
            _context.Devices.Add(device);
        }
        else
        {
            device.TryUpdate(location.Latitude.ToString(CultureInfo.InvariantCulture),
                location.Longitude.ToString(CultureInfo.InvariantCulture),
                request.EndDeviceIds.ApplicationIds.ApplicationId,
                request.UplinkMessage.DecodedPayload.Temperature,
                request.UplinkMessage.DecodedPayload.BarometricPressure,
                request.UplinkMessage.DecodedPayload.RelativeHumidity,
                request.UplinkMessage.DecodedPayload.AnalogIn,
                Convert.ToDateTime(request.UplinkMessage.RxMetadata[0].Time));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpGet(Name = "GetDevices")]
    public async Task<ActionResult<Tuple<List<Device>, int>>> GetDevices([FromQuery]PageInformation pageInformation, CancellationToken cancellationToken)
    {
        var devices = _context.Devices;
        var sortedDevices = (pageInformation.SortBy, pageInformation.SortType) switch
        {
            ("id", "desc") => devices.OrderByDescending(e => e.Id),
            ("id", "asc") => devices.OrderBy(e => e.Id),
            ("applicationId", "desc") => devices.OrderByDescending(e => e.ApplicationId),
            ("applicationId", "asc") => devices.OrderBy(e => e.ApplicationId),
            ("location.latitude", "desc") => devices.OrderByDescending(e => e.Location.Latitude),
            ("location.latitude", "asc") => devices.OrderBy(e => e.Location.Latitude),
            ("location.longitude", "desc") => devices.OrderBy(e => e.Location.Longitude),
            ("location.longitude", "asc") => devices.OrderBy(e => e.Location.Longitude),
            _ => devices.OrderBy(e => e.Id)
        };

        var paginationDevices = await sortedDevices
            .Skip(pageInformation.RowsPerPage * (pageInformation.PageNumber - 1))
            .Take(pageInformation.RowsPerPage).ToListAsync(cancellationToken);
        var deviceQuantity = devices.Count();
        var rtn = new Tuple<List<Device>, int>(paginationDevices, deviceQuantity);
        return Ok(rtn);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<Device>>> GetDevices(CancellationToken cancellationToken)
    {
        var devices = await _context.Devices.ToListAsync(cancellationToken);
        return Ok(devices);
    }
}