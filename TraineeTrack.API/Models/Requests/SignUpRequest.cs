using System.ComponentModel.DataAnnotations;

namespace TraineeTrack.API.Models.Requests;

public class SignUpRequest
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public required string Username { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Email]
    public required string Email { get; set; }
    
    [Required]
    [Password]
    public required string Password { get; set; }
}