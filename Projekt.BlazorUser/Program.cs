using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Projekt.BlazorUser;
using Projekt.BlazorUser.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// rejestracja ProductService w kontenerze zale�no�ci
builder.Services.AddScoped<IReviewService, ReviewService>();

// modyfikacja klienta http aby pobiera� dane z pliku konfiguracyjnego
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress =
 new Uri(builder.Configuration.GetValue<string>("ApplicationAPIUrl"))
});

// rejestruje w kontenerze zale�no�ci polityk� CORS o nazwie SaleKiosk,
// kt�ra zapewnia dost�p do API z dowolnego miejsca oraz przy pomocy dowolnej metody
builder.Services.AddCors(o => o.AddPolicy("FilmWeb", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

await builder.Build().RunAsync();
