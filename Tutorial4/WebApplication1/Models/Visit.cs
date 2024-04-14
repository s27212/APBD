namespace WebApplication1.Models;

public class Visit
{
    public DateTime DateTime { get; set; }
    public int AnimalId { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}