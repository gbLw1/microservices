using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController(
    IPlatformRepo repository,
    IMapper mapper) : ControllerBase
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
    public IActionResult CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = mapper.Map<Platform>(platformCreateDto);

        repository.CreatePlatform(platformModel);
        repository.SaveChanges();

        var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);

        return CreatedAtRoute(
            routeName: nameof(GetPlatformById),
            routeValues: new { platformId = platformReadDto.Id },
            value: platformReadDto
        );
    }
}
