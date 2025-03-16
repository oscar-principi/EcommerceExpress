using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7187";
        options.Audience = "https://localhost:7287"; 
        options.RequireHttpsMetadata = true; 
    });



builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration).AddCacheManager(x => x.WithDictionaryHandle());

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.WithOrigins("https://localhost:7090")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()); 
});

builder.Services.AddAuthorization();

var app = builder.Build();


// Middleware

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{

}

await app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Run();