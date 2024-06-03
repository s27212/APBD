using MedApp.DTO;
using MedApp.Mapper;
using MedApp.Models;
using MedApp.Repositories;

namespace MedApp.Services;

public class MedService : IMedService
{
    private readonly IMedRepository _repository;

    public MedService(IMedRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> AllMedicamentsExist(List<int> idMedicaments)
    {
        return await _repository.AllMedicamentsExist(idMedicaments);
    }

    public async Task<bool> DoctorExists(Doctor doctor)
    {
        return await _repository.DoctorExists(doctor);
    }

    public async Task CreatePrescription(NewPrescriptionForm form)
    {
        if (!await _repository.PatientExists(form.Patient))
        {
            await _repository.AddPatient(form.Patient);
        }
        
        await _repository.AddPrescription(form.MapToPrescription(), form.Medicaments);
    }

    public async Task<Patient?> GetPatientAndPrescriptions(int patientId)
    {
        return await _repository.GetPatientAndPRescriptions(patientId);
    }
}