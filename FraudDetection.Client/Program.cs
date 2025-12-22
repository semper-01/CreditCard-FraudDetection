using FraudDetection.Client;
using FraudDetection.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for calling Server API (same origin when hosted together)
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://localhost:7191/") 
});

// Register ServerFraudClient (uses HttpClient injected above)
builder.Services.AddScoped<ServerFraudClient>();

// Optional: Add logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

await builder.Build().RunAsync();