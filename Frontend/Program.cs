using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://mercantec-quiz.onrender.com"),
    Timeout = TimeSpan.FromSeconds(25),
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any origin
              .AllowAnyHeader()   // Allow any HTTP headers
              .AllowAnyMethod();  // Allow any HTTP methods (GET, POST, etc.)
    });
});

await builder.Build().RunAsync();
