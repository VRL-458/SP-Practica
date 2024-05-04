using LNAT.businesslogic.Managers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"
    )
    .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<PacientesManger>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.EnvironmentName == "QA")
{
    Log.Logger = new LoggerConfiguration()
    .WriteTo.File(app.Configuration.GetSection("Paths").GetSection("Filelocation").Value, rollingInterval: RollingInterval.Day)
    .CreateLogger();
    Log.Information("Initializing the server environment QA!!");
}
else
{
    Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(app.Configuration.GetSection("Paths").GetSection("Filelocation").Value, rollingInterval: RollingInterval.Day)
    .CreateLogger();
    Log.Information("Initializing the server not environment QA !!");
}

// Configure the HTTP request pipeline.
if (app.Environment.EnvironmentName == "QA" || app.Environment.EnvironmentName == "UAT")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
