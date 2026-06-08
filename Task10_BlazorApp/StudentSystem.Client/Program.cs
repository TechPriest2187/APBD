using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentSystem.Client.Services;
using StudentSystem.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<StateContainer>();
builder.Services.AddHttpClient<StudentApiClient>(client => 
    client.BaseAddress = new Uri("http://localhost:5000")); // Match your API's URL

await builder.Build().RunAsync();
