using System.Text.Json.Serialization;

namespace MedApp.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}