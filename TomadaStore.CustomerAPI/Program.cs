using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repositories;
using TomadaStore.CustomerAPI.Repositories.interfaces;
using TomadaStore.CustomerAPI.Services;
using TomadaStore.CustomerAPI.Services.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
