using RabbitMQ.Client;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services;
using TomadaStore.SaleAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<ISaleRepository, SaleRepository>();
builder.Services.AddSingleton<ISaleService, SaleService>();

builder.Services.AddHttpClient("customerAPI", client => client.BaseAddress = new Uri("https://localhost:5001/api/v1/customer/"));

builder.Services.AddHttpClient("productAPI", client => client.BaseAddress = new Uri("https://localhost:6001/api/v1/product/"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
