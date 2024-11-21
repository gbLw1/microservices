using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc;

public class PlatformDataClient(
    IConfiguration configuration,
    IMapper mapper
) : IPlatformDataClient
{
    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Console.WriteLine($"--> Calling gRPC Service: {configuration["GrpcPlatform"]}");
        var channel = GrpcChannel.ForAddress(
            configuration["GrpcPlatform"] ?? throw new ArgumentNullException("GrpcPlatform"));
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllPlatforms(request);
            return mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not call gRPC Server {ex.Message}");
            return [];
        }
    }
}
