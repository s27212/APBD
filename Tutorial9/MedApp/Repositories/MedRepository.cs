using MedApp.Context;
using MedApp.DTO;
using MedApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedApp.Repositories;

public class MedRepository : IMedRepository
{
    private readonly MedDbContext _dbContext;

    public MedRepository(MedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AllMedicamentsExist(List<int> idMedicaments)
    {
        return await _dbContext.Medicaments
            .Where(e => idMedicaments.Contains(e.IdMedicament))
            .Distinct()
            .CountAsync() == idMedicaments.Count;
    }

    public async Task<bool> DoctorExists(Doctor doctor)
    {
        return await _dbContext.Doctors.AnyAsync(e => e.IdDoctor == doctor.IdDoctor);
    }

    public async Task<bool> PatientExists(Patient patient)
    {
        return await _dbContext.Patients.AnyAsync(e => e.IdPatient == patient.IdPatient);
    }

    public async Task AddPatient(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddPrescription(Prescription prescription, List<MedicamentDTO> medicaments)
    {
        var newPrescription = await _dbContext.Prescriptions.AddAsync(prescription);
        await _dbContext.AddRangeAsync(medicaments
            .Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details,
                IdPrescription = newPrescription.Entity.IdPrescription
            }));
        await _dbContext.SaveChangesAsync();
    }
    
}