using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Properties.Contracts;

public class RegisterUserRequest
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}