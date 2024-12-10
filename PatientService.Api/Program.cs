using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientService.Api.Data;

namespace PatientService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurer Entity Framework avec SQL Server
            builder.Services.AddDbContext<PatientDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            // Ajouter les services Identity (si requis pour ton API)
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<PatientDbContext>()
                .AddDefaultTokenProviders();

            // Ajouter les services nécessaires
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Utiliser CORS
            app.UseCors("AllowAll");

            // Configure le pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Authentification et Autorisation
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Swagger et les routes API
            app.MapGet("/", () => Results.Redirect("/swagger"));
            app.MapControllers();

            app.Run();
        }
    }
}
