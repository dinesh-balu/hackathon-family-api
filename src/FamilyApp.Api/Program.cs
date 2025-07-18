using Microsoft.EntityFrameworkCore;
using Serilog;
using FamilyApp.Database;
using FamilyApp.Repositories;
using FamilyApp.Repositories.Interfaces;
using FamilyApp.Services;
using FamilyApp.Services.Interfaces;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/familyapp-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("Default") 
        ?? throw new InvalidOperationException("Connection string 'Default' not found.");

    builder.Services.AddDbContext<FamilyAppDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IChildRepository, ChildRepository>();
    builder.Services.AddScoped<ITherapySessionRepository, TherapySessionRepository>();
    builder.Services.AddScoped<ICareTeamRepository, CareTeamRepository>();
    builder.Services.AddScoped<ISessionProgressRepository, SessionProgressRepository>();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IChildService, ChildService>();
    builder.Services.AddScoped<ITherapySessionService, TherapySessionService>();
    builder.Services.AddScoped<ICareTeamService, CareTeamService>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactNative", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseCors("AllowReactNative");
    app.UseAuthorization();
    app.MapControllers();

    app.MapGet("/api/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }));

    Log.Information("Starting Family App API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
