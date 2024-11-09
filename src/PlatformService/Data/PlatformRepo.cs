using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo(AppDbContext context) : IPlatformRepo
{

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return context.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
        return context.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public void CreatePlatform(Platform plat)
    {
        ArgumentNullException.ThrowIfNull(plat);

        context.Platforms.Add(plat);
    }

    public bool SaveChanges()
    {
        return context.SaveChanges() >= 0;
    }
}
