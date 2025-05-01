using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient(
    "WebApi",
    client => client.BaseAddress = 
        new Uri(builder.Configuration["ApiBaseUrl"]!));

builder.Services.AddScoped(
    sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));

await builder.Build().RunAsync();
