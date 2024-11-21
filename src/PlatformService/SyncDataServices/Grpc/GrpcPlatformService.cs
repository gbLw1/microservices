using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService(
    IPlatformRepo repository,
    IMapper mapper) : GrpcPlatform.GrpcPlatformBase
{
    public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformResponse();
        var platforms = repository.GetAllPlatforms();

        foreach (var plat in platforms)
        {
            response.Platform.Add(mapper.Map<GrpcPlatformModel>(plat));
        }

        return Task.FromResult(response);
    }
}