using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SystemBox.API.ExceptionMiddleware;
using SystemBox.API.Extensions;
using SystemBox.Data;
using SystemBox.Data.Identity;
using SystemBox.Domain.Models;
using SystemBox.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<SystemDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
DependencyInjectionConfig.ConfigureServicesIoc(builder.Services);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseSwaggerDocumentation(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var systemDbContext = services.GetRequiredService<SystemDbContext>();
var userManager = services.GetRequiredService<UserManager<Usuario>>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await systemDbContext.Database.MigrateAsync();
    await SystemDbContextSeed.SeedUserAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "um erro ocorreu durante a migration.");
}

app.Run();
