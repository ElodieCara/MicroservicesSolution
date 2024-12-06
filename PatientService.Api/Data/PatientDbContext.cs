using Microsoft.EntityFrameworkCore;
using PatientService.Api.Models;
using System.Collections.Generic;

namespace PatientService.Api.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
    }
}
