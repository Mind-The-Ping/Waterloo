using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Waterloo.Options;
using Waterloo.Repository.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;
using Waterloo.Repository.Station;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddScoped<LineRepository>();
builder.Services.AddScoped<RouteRepository>();
builder.Services.AddScoped<StationRepository>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();


builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

    var client = new MongoClient(options.ConnectionString);
    var database = client.GetDatabase(options.Name);

    return database;
});

builder.Build().Run();
