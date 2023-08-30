using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SystemMonitor;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Services;
using SystemMonitor.Services.Controllers;

LoadDotEnv();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var authenticationActive = AddAuthentication(builder.Services);
AddSwaggerGen(builder.Services);
AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    AddSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (authenticationActive)
{
    app.MapControllers();
}
else
{
    app.MapControllers().AllowAnonymous();
}

app.Run();

return;

void LoadDotEnv()
{
    var root = Directory.GetCurrentDirectory();
    var dotenv = Path.Combine(root, ".env");
    DotEnv.Load(dotenv);
}

void AddSwaggerGen(IServiceCollection services)
{
    if (!authenticationActive)
    {
        services.AddSwaggerGen();
        return;
    }

    var swaggerUrlAuth = Environment.GetEnvironmentVariable(EnvironmentVariables.SwaggerUrlAuth);
    var swaggerUrlToken = Environment.GetEnvironmentVariable(EnvironmentVariables.SwaggerUrlToken);

    if (string.IsNullOrWhiteSpace(swaggerUrlAuth) ||
        string.IsNullOrWhiteSpace(swaggerUrlToken))
    {
        services.AddSwaggerGen();
        return;
    }

    services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("OIDC", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(swaggerUrlAuth),
                    TokenUrl = new Uri(swaggerUrlToken),
                    Scopes = new Dictionary<string, string>
                    {
                        {"openid", "Standard OpenID Connect scope"}
                    }
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "OIDC"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

bool AddAuthentication(IServiceCollection services)
{
    var authority = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthAuthority);
    var audience = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthAudience);

    if (string.IsNullOrWhiteSpace(authority) || string.IsNullOrWhiteSpace(audience))
    {
        return false;
    }

    services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = audience;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ValidateAudience = true,
                ValidateTokenReplay = true
            };
        });

    return true;
}

void AddServices(IServiceCollection services)
{
    services.AddScoped<IMemoryControllerService, MemoryControllerService>();
    services.AddScoped<IDiskControllerService, DiskControllerService>();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        services.AddScoped<IDiskService, UnixDiskService>();
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

void AddSwaggerUi()
{
    var swaggerClientId = Environment.GetEnvironmentVariable(EnvironmentVariables.SwaggerClientId);
    var swaggerClientSecret = Environment.GetEnvironmentVariable(EnvironmentVariables.SwaggerClientSecret);

    app.UseSwaggerUI(options =>
    {
        if (swaggerClientId != null)
        {
            options.OAuthClientId(swaggerClientId);

            if (swaggerClientSecret != null)
            {
                options.OAuthClientSecret(swaggerClientSecret);
            }
        }

        options.OAuthUsePkce();
    });
}