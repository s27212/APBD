using MedApp.DTO;
using MedApp.Models;

namespace MedApp.Services;

public interface IMedService
{
    Task<bool> AllMedicamentsExist(List<int> idMedicaments);
    Task<bool> DoctorExists(Doctor doctor);
    Task CreatePrescription(NewPrescriptionForm form);
}