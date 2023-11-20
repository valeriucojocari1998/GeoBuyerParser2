using GeoBuyerParser2.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoBuyerParser2.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly OSMService _osmService;

    public ApiController(ILogger<ApiController> logger, OSMService oSMService)
    {
        _logger = logger;
        _osmService = oSMService;
    }

    [HttpGet("api/ParseLocations/{CountryCode}")]
    public async Task<IActionResult> Parse([FromRoute] string CountryCode)
    {
        var data = await _osmService.ParseData(CountryCode);
        return Ok(data);
    }

    [HttpGet("api/GetLocationsFromDB")]
    public IActionResult Get()
    {
        var data = _osmService.GetData();
        return Ok(data);
    }
}