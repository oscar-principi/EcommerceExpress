using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Agregar Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Configurar el servicio HttpClient sin bloquear el hilo principal
builder.Services.AddScoped<HttpClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7287") };

    // La configuración del token se debe hacer dentro de AuthenticationService
    return httpClient;
});

// Agregar soporte para autorización en Blazor
builder.Services.AddAuthorizationCore();

// Configurar los servicios de autenticación correctamente
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<AuthenticationService>();

// Construir y ejecutar la aplicación
await builder.Build().RunAsync();



//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Frontend;
//using Microsoft.AspNetCore.Components.Authorization;
//using Blazored.LocalStorage;
//using System.Net.Http.Headers;
//using Frontend.Services;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddScoped(sp =>
//{
//    var localStorage = sp.GetRequiredService<ILocalStorageService>();
//    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7287") };

//    var token = localStorage.GetItemAsync<string>("authToken").Result;
//    if (!string.IsNullOrEmpty(token))
//    {
//        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//    }

//    return httpClient;
//});


//builder.Services.AddAuthorizationCore();

//builder.Services.AddScoped<CustomAuthenticationStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
//builder.Services.AddScoped<AuthenticationService>();


//await builder.Build().RunAsync();