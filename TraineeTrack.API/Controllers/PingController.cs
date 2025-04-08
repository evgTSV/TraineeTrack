using Microsoft.AspNetCore.Mvc;

namespace TraineeTrack.API.Controllers;

[ApiController]
public class PingController : ControllerBase
{
    [HttpGet("ping")]
    public string Ping()
    {
        return "pong";
    }
}