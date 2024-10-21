using Microsoft.EntityFrameworkCore;
using WindowsServicesCheck;
using WindowsServicesCheck.Data;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //services.AddDbContext<ServiceNameContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ServiceName.Data;Trusted_Connection=True;MultipleActiveResultSets=true"));
    })
    .Build();

await host.RunAsync();
