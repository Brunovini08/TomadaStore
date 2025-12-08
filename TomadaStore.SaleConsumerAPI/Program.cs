using TomadaStore.SaleConsumerAPI.Data;
using TomadaStore.SaleConsumerAPI.Repositories;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;
using TomadaStore.SaleConsumerAPI.Services;
using TomadaStore.SaleConsumerAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<ISaleConsumerRepository, SaleConsumerRepository>();
builder.Services.AddSingleton<ISaleConsumerService, SaleConsumerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
