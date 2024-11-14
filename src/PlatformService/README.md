# Platform Service

Some notes about the Platform Service.

## Generating Migrations

To generate a new migration, we need to do a little hack.

This is because we are using the in-memory database for development/testing purposes and the migrations are not supported with the in-memory database.

So, to work around this issue, we need to use the SQL Server database for migrations, and then switch back to the in-memory database.

To do this, we need to comment out this block in the [`Program.cs`](./Program.cs) file:

```csharp
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
```

and also the `PrepDb` method in the [`Program.cs`](./Program.cs) file:

```csharp
PrepDb.PrepPopulation(app, app.Environment.IsProduction());
```

Then, we need to run the following commands:

```bash
dotnet ef migrations add <MigrationName>
```

After that, just uncomment things back and we are good to go.
