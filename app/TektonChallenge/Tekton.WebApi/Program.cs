using Serilog;
using Tekton.Application;
using Tekton.Infrastructure;
using Tekton.WebApi.HealthChecks;
using Tekton.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding health check service.
builder.Services.AddHealthChecks()
	.AddCheck<TektonHealthCheck>("MyHealth");

Log.Logger = new LoggerConfiguration()
	.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// mapping health check endpoint
app.MapHealthChecks("/health");

app.Run();
