using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Application.Behaviors;
using Shop.Infrastructure;
using Shop.Infrastructure.DbContexts;
using Shop.Infrastructure.Repositories.Products.Interfaces;
using Shop.Infrastructure.Repositories.Products.Repositories;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var connestionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ShopAPIDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql(connestionString, b => b.MigrationsAssembly("Shop.API"));

    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
    if (loggerFactory != null)
    {
        options.UseLoggerFactory(loggerFactory);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();

        // var loggingDbCommandInterceptor = serviceProvider.GetService<LoggingDbCommandInterceptor>();
        // var loggingDbTransactionInterceptor = serviceProvider.GetService<LoggingDbTransactionInterceptor>();
        // if (loggingDbCommandInterceptor != null && loggingDbTransactionInterceptor != null)
        // {
        //     options.AddInterceptors(loggingDbCommandInterceptor, loggingDbTransactionInterceptor);
        // }
    }
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
foreach (var currentAssembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(currentAssembly));
}
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddScoped<ShopAPIDbContext>(provider => provider.GetService<ShopAPIDbContext>());
builder.Services.AddScoped<IProductQueryRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandRepository, ProductRepository>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.NullValueHandling
                = NullValueHandling.Ignore;

            options.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
        })
    .AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    });

Assembly assembly = Assembly.GetExecutingAssembly();
System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

builder.Services.AddEndpointsApiExplorer();
builder.
    Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop.API", Version = fvi.FileVersion });
        c.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });
        c.DocInclusionPredicate((name, api) => true);
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"ApiKey Authorization header using the token scheme.
                 </br>Enter 'Just_For_Fun'.
                 </br>Example: 'Just_For_Fun'",
            Name = "authToken",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "ApiKey"
        });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSwagger();

app.MapControllers();

app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Swagger Documentation v1"));

app.Run();
