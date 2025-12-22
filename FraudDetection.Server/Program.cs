using FraudDetection.Server.Services;
using FraudDetection.Shared.Contracts;
using FraudDetection.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Read FastAPI base URL from config (ensure a scheme, default to http)
var fastApiBaseUrl = builder.Configuration["FastApi:BaseUrl"] ?? "https://localhost:7191";
if (!fastApiBaseUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
    !fastApiBaseUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
{
    fastApiBaseUrl = "http://" + fastApiBaseUrl;
}

// DI: typed HttpClient for IFraudService -> FraudService (registers the implementation)
builder.Services.AddHttpClient<IFraudService, FraudService>(client =>
{
    client.BaseAddress = new Uri(fastApiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Add CORS for Blazor WebAssembly client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7028", "http://localhost:5023")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fraud Detection API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Use CORS before routing
app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"✅ Server started. FastAPI service at: {fastApiBaseUrl}");

app.Run();