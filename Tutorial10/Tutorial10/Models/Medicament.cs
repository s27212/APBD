using System.Text.Json.Serialization;

namespace MedApp.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Type { get; set; }

    [JsonIgnore]
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } =
        new List<PrescriptionMedicament>();
}