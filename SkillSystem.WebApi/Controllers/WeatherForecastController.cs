using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkillSystem.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("public")]
    public ActionResult<string> GetPublic()
    {
        return Ok("Public");
    }

    [Authorize]
    [HttpGet("secret")]
    public ActionResult<int> GetSecret()
    {
        return Ok("Secret");
    }
}