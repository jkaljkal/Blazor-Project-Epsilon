using Microsoft.EntityFrameworkCore;
using BlazorApp.Components;
using BlazorApp.Data;
using BlazorApp.Repositories.Implementations;
using BlazorApp.Repositories.Intefaces;
using BlazorApp.Shared.Models.Entites;
using BlazorApp.Services.Implementations;
using BlazorApp.Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<WeatherForecastService>();

// Add controllers, repositories and dbcontext
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPrintNameService, PrintNameService>();

builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddLogging();
builder.Services.AddHttpClient();

var app = builder.Build();

// test Employee/Manager class
var serviceProvider = builder.Services.BuildServiceProvider();
serviceProvider.GetRequiredService<IPrintNameService>()
    .PrintName(new Employee("John Kalogeropoulos"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp.Client._Imports).Assembly);

app.Run();
