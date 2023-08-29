using System.Runtime.InteropServices;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Services;
using SystemMonitor.Services.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

return;

void AddServices(IServiceCollection services)
{
    services.AddScoped<IMemoryControllerService, MemoryControllerService>();
    services.AddScoped<IDiskControllerService, DiskControllerService>();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        services.AddScoped<IMemoryService, UnixMemoryService>();
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        services.AddScoped<IMemoryService, WindowsMemoryService>();
        services.AddScoped<IDiskService, WindowsDiskService>();
    }
    else
    {
        throw new OperatingSystemNotSupportedException(RuntimeInformation.OSDescription);
    }
}