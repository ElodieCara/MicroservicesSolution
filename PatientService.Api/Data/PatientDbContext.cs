using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientService.Api.Models;

namespace PatientService.Api.Data
{
    public class PatientDbContext : IdentityDbContext<IdentityUser>
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }

        // Ta table personnalisée pour les patients
        public DbSet<Patient> Patients { get; set; }
    }
}
