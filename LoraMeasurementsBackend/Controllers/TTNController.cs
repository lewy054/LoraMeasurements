using Microsoft.AspNetCore.Mvc;

namespace LoraMeasurementsBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class TTNController : ControllerBase
{
    private readonly ILogger<TTNController> _logger;

    public TTNController(ILogger<TTNController> logger)
    {
        _logger = logger;
    }
}