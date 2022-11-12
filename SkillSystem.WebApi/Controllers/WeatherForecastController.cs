using Microsoft.AspNetCore.Mvc;

namespace SkillSystem.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet()]
    public ActionResult<int> Get()
    {
        return Ok(42);
    }
}
