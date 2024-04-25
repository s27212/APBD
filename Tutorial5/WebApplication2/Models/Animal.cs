using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Animal
{
    public int IdAnimal { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string Area { get; set; }
    
}