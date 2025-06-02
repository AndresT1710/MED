using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SMED.FrontEnd;
using SMED.FrontEnd.Services;
using Blazored.LocalStorage;
using System.Net.Http;




var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuraci�n del HttpClient con timeout extendido
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        //BaseAddress = new Uri("https://localhost:59166/"),
        BaseAddress = new Uri("https://localhost:7009/"),
        Timeout = TimeSpan.FromSeconds(30)
    };

    // Headers por defecto
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

    return httpClient;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ClinicalHistoryService>();
builder.Services.AddScoped<DiseaseService>();
builder.Services.AddScoped<PersonalHistoryService>();
builder.Services.AddScoped<SurgeryService>();
builder.Services.AddScoped<SurgeryHistoryService>();
builder.Services.AddScoped<HabitService>();
builder.Services.AddScoped<HabitHistoryService>();
builder.Services.AddLogging();



await builder.Build().RunAsync();