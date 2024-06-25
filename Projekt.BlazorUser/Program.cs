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
 new Uri(builder.Configuration.GetValue<string>("ApplicationAPIUrl"))
});

// rejestruje w kontenerze zale¿noœci politykê CORS o nazwie SaleKiosk,
// która zapewnia dostêp do API z dowolnego miejsca oraz przy pomocy dowolnej metody
builder.Services.AddCors(o => o.AddPolicy("FilmWeb", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

await builder.Build().RunAsync();
