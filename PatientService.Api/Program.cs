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

            // Ajouter les services nécessaires
            builder.Services.AddControllers(); // Pour les API Controllers
            builder.Services.AddEndpointsApiExplorer(); // Pour Swagger
            builder.Services.AddSwaggerGen(); // Documentation API

            var app = builder.Build();

            // Configure le pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Génération Swagger uniquement en dev
                app.UseSwaggerUI(); // Interface Swagger
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Redirige la page d'accueil vers Swagger
            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.MapControllers(); // Mappe les routes API

            app.Run();
        }
    }
}
