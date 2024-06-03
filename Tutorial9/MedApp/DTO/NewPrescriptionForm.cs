using MedApp.Models;

namespace MedApp.DTO;

public class NewPrescriptionForm
{
    public required Patient Patient { get; set; }
    
    public required Doctor Doctor { get; set; }
    
    public required List<MedicamentDTO> Medicaments { get; set; }
    
    public required DateTime Date { get; set; }
    
    public required DateTime DueDate { get; set; }
}