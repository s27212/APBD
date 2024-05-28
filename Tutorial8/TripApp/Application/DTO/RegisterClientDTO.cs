using System.ComponentModel.DataAnnotations;

namespace TripApp.Application.DTO;

public class RegisterClientDTO
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string Telephone { get; set; }
    
    [Required]
    [Length(maximumLength:11, minimumLength:11)]
    public string Pesel { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? PaymentDate { get; set; }
}