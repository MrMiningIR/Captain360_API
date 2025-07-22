using Capitan360.Api.Extensions;
using Capitan360.Api.Middlewares;
using Capitan360.Api.RequestHelpers.Authorization;
using Capitan360.Application.Extensions;
using Capitan360.Infrastructure.Extensions;
using Capitan360.Infrastructure.Seeders;
using Finbuckle.MultiTenant;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region ServiceExtensions

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<CompanyIdAuthorizationFilter>();

#endregion ServiceExtensions

// Load Permissions from Database
await builder.Services.AddPoliciesFromDatabaseAsync();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("https://localhost:7045") // دامنه Blazor
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); //

        //corsPolicyBuilder // دامنه Blazor
        //    .AllowAnyMethod()
        //    .AllowAnyHeader()
        //    .AllowCredentials(); // Cookies
    });
});

var app = builder.Build();

app.UseMultiTenant();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<IPrimaryInformationSeeder>();
    await context.SeedDataAsync(new CancellationToken(), Assembly.GetExecutingAssembly());
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
    throw new Exception($"An error occurred while seeding the Database.{ex.Message}");
}

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");

// Middleware for multi-tenancy

app.UseAuthentication();
//app.UseMiddleware<TokenValidationMiddleware>(); //
app.UseMiddleware<PermissionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();