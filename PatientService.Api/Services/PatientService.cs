using PatientService.Api.Data;
using PatientService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PatientService.Api.Services
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly PatientDbContext _context;

        public PatientServiceImpl(PatientDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(int id, Patient updatedPatient)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.FirstName = updatedPatient.FirstName;
                patient.LastName = updatedPatient.LastName;
                patient.DateOfBirth = updatedPatient.DateOfBirth;
                patient.Gender = updatedPatient.Gender;
                patient.Address = updatedPatient.Address;
                patient.PhoneNumber = updatedPatient.PhoneNumber;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
