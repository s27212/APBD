using MedApp.DTO;
using MedApp.Models;

namespace MedApp.Mapper;

public static class PrescriptionMapper
{
    public static Prescription MapToPrescription(this NewPrescriptionForm form)
    {
        return new Prescription
        {
            IdDoctor = form.Doctor.IdDoctor,
            IdPatient = form.Patient.IdPatient,
            Doctor = form.Doctor,
            Patient = form.Patient,
            Date = form.Date,
            DueDate = form.DueDate
        };
    }
}