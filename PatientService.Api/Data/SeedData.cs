using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace PatientService.Api.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Vérifie et crée le rôle "Admin"
            const string adminRoleName = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
                Console.WriteLine($"Rôle '{adminRoleName}' créé avec succès.");
            }

            // Vérifie et crée l'utilisateur admin
            const string adminUserName = "admin";
            const string adminPassword = "Admin@123!";
            var adminUser = await userManager.FindByNameAsync(adminUserName);

            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = adminUserName,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRoleName);
                    Console.WriteLine($"Administrateur '{adminUserName}' créé avec succès.");
                }
                else
                {
                    throw new Exception($"Erreur lors de la création de l'utilisateur admin : {string.Join(", ", result.Errors)}");
                }
            }
            else
            {
                Console.WriteLine($"Administrateur '{adminUserName}' existe déjà.");
            }
        }
    }
}
