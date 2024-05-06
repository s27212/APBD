namespace Tutorial6.Models;

public class OrderDTO
{
    public int IdOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
}