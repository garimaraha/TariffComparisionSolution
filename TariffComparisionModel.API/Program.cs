using TariffComparisionModel.API.ExceptionHandler;
using TariffComparisionModel.Factories;
using TariffComparisionModel.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register the factory for tariff comparison as a singleton service

builder.Services.AddSingleton<ITariffComparisionFactory, TariffComparisionFactory>();

// Register the tariff comparison service as a transient service

builder.Services.AddTransient<ITariffComparisionService, TariffComparisionService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.UseExceptionHandler();
app.UseStatusCodePages();


app.Run();
public partial class Program { } // Partial class helps execute integration tests for network-level request validation without affecting the main application
