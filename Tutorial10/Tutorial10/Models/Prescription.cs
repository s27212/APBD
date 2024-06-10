using System.Text.Json.Serialization;

namespace MedApp.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }

    [JsonIgnore]
    public Patient Patient { get; set; } = null!;

    public int IdDoctor { get; set; }

    [JsonIgnore] 
    public Doctor Doctor { get; set; } = null!;
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } =
        new List<PrescriptionMedicament>();
}