using CommandsService.Models;

namespace CommandsService.Data;

public class CommandRepo(AppDbContext context) : ICommandRepo
{
    public IEnumerable<Platform> GetAllPlatforms()
    {
        return context.Platforms.ToList();
    }

    public void CreatePlatform(Platform plat)
    {
        ArgumentNullException.ThrowIfNull(plat);

        context.Platforms.Add(plat);
    }

    public bool PlatformExists(int platformId)
    {
        return context.Platforms.Any(p => p.Id == platformId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.PlatformId);
    }

    public Command? GetCommand(int platformId, int commandId)
    {
        return context.Commands
            .Where(c => c.PlatformId == platformId && c.Id == commandId)
            .FirstOrDefault();
    }

    public void CreateCommand(int platformId, Command command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.PlatformId = platformId;
        context.Commands.Add(command);
    }

    public bool SaveChanges()
    {
        return context.SaveChanges() >= 0;
    }
}
