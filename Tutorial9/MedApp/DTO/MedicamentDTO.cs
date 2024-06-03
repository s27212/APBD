namespace MedApp.DTO;

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public required string Details { get; set; }
}