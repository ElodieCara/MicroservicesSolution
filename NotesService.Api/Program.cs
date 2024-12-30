using MongoDB.Driver;
using NotesService.Api.Data;
using NotesService.Api.Models;
using NotesService.Api.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NotesService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            

            // Configure MongoDB settings and services
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoDbSettings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(mongoDbSettings.ConnectionString);
            });

            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddSingleton<NotesManagementService>();

            // JWT configuration
            var key = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("La clé JWT n'est pas configurée dans appsettings.json.");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // CORS configuration
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

            // Enable CORS
            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet("/", () => Results.Redirect("/swagger"));

            // Map controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
