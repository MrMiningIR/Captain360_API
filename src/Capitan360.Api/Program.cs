using Capitan360.Api.Extensions;
using Capitan360.Api.Middlewares;
using Capitan360.Application.Extensions;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Authorization.Services;
using Capitan360.Infrastructure.Extensions;
using Capitan360.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();




builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


// Load Permissions from Database
await builder.Services.AddPoliciesFromDatabaseAsync(); //Todo : needs to review!



var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<IPrimaryInformationSeeder>();
    await context.SeedDataAsync(new CancellationToken());
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}


//var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeders>();
//await seeder.SeedData();


app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<TokenValidationMiddleware>(); // TODO : needs to review!
app.UseMiddleware<PermissionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();