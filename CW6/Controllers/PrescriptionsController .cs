using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CW6.Context;
using CW6.Models;
using CW6.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly ApbdDbContext _context;

    public PrescriptionsController(ApbdDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionDto prescriptionDto)
    {
        if (prescriptionDto.Medicaments.Count > 10)
            return BadRequest("Prescription can not contain more than 10 medicaments.");

        if (prescriptionDto.DueDate < prescriptionDto.Date)
            return BadRequest("DueDate must be greater than or equal to Date.");

        var patient = await _context.Patient.FirstOrDefaultAsync(p => p.FirstName == prescriptionDto.Patient.FirstName && p.LastName == prescriptionDto.Patient.LastName && p.Birthdate == prescriptionDto.Patient.Birthdate);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                Birthdate = prescriptionDto.Patient.Birthdate
            };
            _context.Patient.Add(patient);
        }

        var doctor = await _context.Doctor.FindAsync(prescriptionDto.Doctor.IdDoctor);
        if (doctor == null)
            return BadRequest("Doctor not found.");

        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            IdPatientNavigation = patient,
            IdDoctorNavigation = doctor,
            PrescriptionMedicament = new List<PrescriptionMedicament>()
        };

        foreach (var med in prescriptionDto.Medicaments)
        {
            var medicament = await _context.Medicament.FindAsync(med.IdMedicament);
            if (medicament == null)
                return BadRequest("Medicament not found.");

            prescription.PrescriptionMedicament.Add(new PrescriptionMedicament
            {
                IdMedicamentNavigation = medicament,
                Dose = med.Dose,
                Details = med.Description
            });
        }

        _context.Prescription.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDetailsDto>> GetPatientDetails(int id)
    {
        var patient = await _context.Patient
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.IdDoctorNavigation)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicament)
            .ThenInclude(pm => pm.IdMedicamentNavigation)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
        {
            return NotFound("Patient not found.");
        }

        var patientDetails = new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(pr => new PrescriptionDetailsDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new DoctorDetailsDto()
                {
                    IdDoctor = pr.IdDoctorNavigation.IdDoctor,
                    FirstName = pr.IdDoctorNavigation.FirstName,
                    LastName = pr.IdDoctorNavigation.LastName,
                    Email = pr.IdDoctorNavigation.Email
                },
                Medicaments = pr.PrescriptionMedicament.Select(pm => new MedicamentDetailsDto
                {
                    IdMedicament = pm.IdMedicamentNavigation.IdMedicament,
                    Name = pm.IdMedicamentNavigation.Name,
                    Description = pm.IdMedicamentNavigation.Description,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList()
            }).OrderByDescending(pr => pr.DueDate).ToList()
        };

        return Ok(patientDetails);
    }
}
