using BlazorApp;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedClass;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7170/") });

builder.Services.AddScoped<BaseApi>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddBlazoredToast();
await builder.Build().RunAsync();