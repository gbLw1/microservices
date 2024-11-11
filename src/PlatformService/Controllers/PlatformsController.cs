using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController(
    IPlatformRepo repository,
    IMapper mapper,
    ICommandDataClient commandDataClient) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms from PlatformService");

        var platforms = repository.GetAllPlatforms();

        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{platformId}", Name = nameof(GetPlatformById))]
    public IActionResult GetPlatformById(int platformId)
    {
        var platform = repository.GetPlatformById(platformId);

        if (platform is null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = mapper.Map<Platform>(platformCreateDto);

        repository.CreatePlatform(platformModel);
        repository.SaveChanges();

        var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await commandDataClient.SendPlatformToCommand(plat: platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }

        return CreatedAtRoute(
            routeName: nameof(GetPlatformById),
            routeValues: new { platformId = platformReadDto.Id },
            value: platformReadDto
        );
    }
}
