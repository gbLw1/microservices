using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController(
    ICommandRepo repository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCommandsForPlatform([FromRoute] int platformId)
    {
        Console.WriteLine($"--> Getting Commands for Platform: {platformId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var commands = repository.GetCommandsForPlatform(platformId);
        return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
    public IActionResult GetCommandForPlatform([FromRoute] int platformId, [FromRoute] int commandId)
    {
        Console.WriteLine($"--> Getting Command: {commandId} for Platform: {platformId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var command = repository.GetCommand(
            platformId: platformId,
            commandId: commandId);

        if (command is null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public IActionResult CreateCommandForPlatform(
        [FromRoute] int platformId,
        [FromBody] CommandCreateDto commandDto)
    {
        Console.WriteLine($"--> Creating Command for Platform: {platformId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var command = mapper.Map<Command>(commandDto);

        repository.CreateCommand(platformId, command);
        repository.SaveChanges();

        var commandReadDto = mapper.Map<CommandReadDto>(command);
        return CreatedAtRoute(
            nameof(GetCommandForPlatform),
            new { platformId = platformId, commandId = commandReadDto.Id },
            commandReadDto);
    }
}
