using BlazorApp;
using BlazorApp.HttpServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7170/") });
builder.Services.AddScoped<ProjectHttpService>();
builder.Services.AddScoped<ModuleHttpService>();
builder.Services.AddScoped<IssueLogHttpService>();
builder.Services.AddScoped<SolveSheetHttpService>();
builder.Services.AddScoped<UserAccountHttpService>();

builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();