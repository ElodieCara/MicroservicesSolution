using Microsoft.AspNetCore.Mvc;
using PatientService.Api.Data;
using PatientService.Api.Models;

namespace PatientService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientDbContext _context;

        public PatientController(PatientDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Patients.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _context.Patients.Find(id);
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Patient updatedPatient)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Gender = updatedPatient.Gender;
            patient.Address = updatedPatient.Address;
            patient.PhoneNumber = updatedPatient.PhoneNumber;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
