using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTO;

public class AddProductDTO
{
    [Required]
    public int? IdProduct { get; set; }
    
    [Required]
    public int? IdWarehouse { get; set; }
    
    [Required] 
    [Range(1,int.MaxValue, ErrorMessage = "Amount should be positive.")]
    public int? Amount { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; }
}