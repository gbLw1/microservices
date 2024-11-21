using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Comment the if statement to use only the SQL Server DB to generate migrations
if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SQL Server DB");
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn"));
    });
}
else
{
    Console.WriteLine("--> Using InMem DB");
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("InMem");
    });
}

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Sync services
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddGrpc();

// Async services
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

// Check env variables
Console.WriteLine($"--> Command Service Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Get rid of warnings since we are not using HTTPS into our internal k8s cluster
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();

// Optional: add a endpoint to serve up our contract
app.MapGet("/protos/platforms.proto", async context =>
{
    var file = Path.Combine(app.Environment.ContentRootPath, "Protos", "platforms.proto");
    var content = File.ReadAllText(file);
    await context.Response.WriteAsync(content);
});

PrepDb.PrepPopulation(app, app.Environment.IsProduction()); // Comment to generate migrations

app.Run();
