using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Behaviors;
using Shop.ClientService.Infrastructure;
using Shop.ClientService.Services;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories.Client.Interfaces;
using Shop.Infrastructure.Repositories.Client.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var connestionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClientServiceDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql(connestionString, b => b.MigrationsAssembly("Shop.API"));

    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
    if (loggerFactory != null)
    {
        options.UseLoggerFactory(loggerFactory);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
foreach (var currentAssembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(currentAssembly));
}
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddScoped<ClientServiceDbContext>(provider => provider.GetService<ClientServiceDbContext>());
builder.Services.AddScoped<IClientQueryRepository, ClientRepository>();
builder.Services.AddScoped<IClientCommandRepository, ClientRepository>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ClientServiceGrpc>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();