using Frontend.Components;
using Blazored.LocalStorage;
using System.Net.Http;
using Frontend;
using OfficeOpenXml;


var builder = WebApplication.CreateBuilder(args);


// Set the license context to non-commercial (add this line here if using EPPlus)
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<HttpClient>(sp => new HttpClient 
{
    BaseAddress = new Uri ("https://mercantec-quiz.onrender.com")
});

// In Program.cs (for .NET 6.0 and later):
builder.Services.AddHttpContextAccessor();

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Use the CORS policy
app.UseCors("AllowAll");

app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


