using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Projekt.BlazorUser;
using Projekt.BlazorUser.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// rejestracja ProductService w kontenerze zale¿noœci
builder.Services.AddScoped<IReviewService, ReviewService>();

// modyfikacja klienta http aby pobiera³ dane z pliku konfiguracyjnego
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress =
 new Uri(builder.Configuration.GetValue<string>("SaleKioskAPIUrl"))
});

await builder.Build().RunAsync();
