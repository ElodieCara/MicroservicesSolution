using PatientService.Api.Models;

namespace PatientService.Api.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(int id, Patient updatedPatient);
        Task DeletePatientAsync(int id);
    }
}
