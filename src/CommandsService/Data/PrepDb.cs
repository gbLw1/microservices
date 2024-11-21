using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data;

public class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetRequiredService<IPlatformDataClient>();
        var platforms = grpcClient.ReturnAllPlatforms();

        var repo = serviceScope.ServiceProvider.GetRequiredService<ICommandRepo>();
        SeedData(repo, platforms);
    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
        Console.WriteLine("--> Seeding new platforms...");

        foreach (var platform in platforms)
        {
            if (!repo.ExternalPlatformExists(platform.ExternalId))
            {
                repo.CreatePlatform(platform);
            }
        }

        repo.SaveChanges();
    }
}
