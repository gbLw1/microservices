using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("c/[controller]")]
[ApiController]
public class PlatformsController(
    ICommandRepo repository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms from Commands Service");

        var platformItems = repository.GetAllPlatforms();
        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }

    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound test of from Platforms Controller");
    }
}
