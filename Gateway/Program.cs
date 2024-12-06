using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Charger le fichier ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ajouter CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Remplace par l'URL de ton frontend
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Ajouter Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

// Utiliser CORS
app.UseCors("AllowAll");

// Utiliser Ocelot comme middleware
await app.UseOcelot();

app.Run();
