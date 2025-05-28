using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SMED.FrontEnd;
using SMED.FrontEnd.Services;
using Blazored.LocalStorage;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// URL del BACKEND
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7009")  // <== Puerto backend real
});


builder.Services.AddBlazoredLocalStorage();
// SERVICES
builder.Services.AddScoped<PersonService>();

await builder.Build().RunAsync();
