using MongoDB.Driver;
using NotesService.Api.Data;
using NotesService.Api.Models;
using Microsoft.Extensions.Options;
using NotesService.Api.Services;

namespace NotesService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure MongoDB settings from appsettings.json
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            // Add MongoClient as a singleton
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoDbSettings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(mongoDbSettings.ConnectionString);
            });

            // Add MongoDbService as a singleton
            builder.Services.AddSingleton<MongoDbService>();

            // Add NotesManagementService as a singleton
            builder.Services.AddSingleton<NotesManagementService>();

            // Ajout de la configuration CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") 
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Activez CORS dans le pipeline HTTP
            app.UseCors("AllowReactApp");

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
