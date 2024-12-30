using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatientService.Api.Data;
using PatientService.Api.Services;
using System.Text;
using Microsoft.OpenApi.Models;

namespace PatientService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configurer Entity Framework avec SQL Server avec retry on failure
            builder.Services.AddDbContext<PatientDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()));

            // 2. Ajouter les services Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<PatientDbContext>()
                .AddDefaultTokenProviders();

            // 3. Enregistrer les services avec leurs interfaces
            builder.Services.AddScoped<IPatientService, PatientServiceImpl>();

            // 4. Vérifier les paramètres JWT chargés
            var jwtKey = builder.Configuration["Jwt:Key"];
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            Console.WriteLine($"Chargement des paramètres : Jwt:Key = {jwtKey}");
            Console.WriteLine($"Chargement des paramètres : Jwt:Issuer = {jwtIssuer}");
            Console.WriteLine($"Chargement des paramètres : Jwt:Audience = {jwtAudience}");

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured in appsettings.json");
            }

            // 5. Configurer l'authentification JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            // 6. Ajouter CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Remplace par l'URL de ton frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // 7. Ajouter les services nécessaires
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PatientService API",
                    Version = "v1",
                    Description = "API pour gérer les patients et l'authentification."
                });

                // Ajouter la configuration JWT pour Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Entrez 'Bearer' suivi de votre token JWT.\n\nExemple : 'Bearer eyJhbGc...'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Vérification de l'existence de l'utilisateur admin
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                // Récupérer le username de l'admin depuis la configuration
                var adminUserName = builder.Configuration["AdminUser:UserName"];
                if (string.IsNullOrEmpty(adminUserName))
                {
                    Console.WriteLine("Attention : Le nom d'utilisateur de l'administrateur n'est pas configuré dans appsettings.json.");
                }
                else
                {
                    // Vérifiez si l'utilisateur admin existe par le UserName
                    var adminUser = await userManager.FindByNameAsync(adminUserName);

                    if (adminUser == null)
                    {
                        Console.WriteLine($"Attention : L'utilisateur administrateur avec le nom d'utilisateur '{adminUserName}' n'existe pas !");
                        // Optionnel : Vous pourriez créer un administrateur ici si nécessaire.
                    }
                    else
                    {
                        Console.WriteLine($"Administrateur trouvé : {adminUser.UserName}");
                    }
                }
            }

            // 8. Utiliser les fichiers statiques
            app.UseStaticFiles();

            // 9. Utiliser CORS
            app.UseCors("AllowAll");

            // 10. Authentification et Autorisation
            app.UseAuthentication();
            app.UseAuthorization();

            // 11. Configure le pipeline HTTP pour Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatientService API V1");
                    c.RoutePrefix = string.Empty; // Swagger à la racine
                });
            }

            // 12. Map Controllers
            app.MapControllers();

            // 13. Lancer l'application
            await app.RunAsync();
        }


    }
}
