using System.ComponentModel.DataAnnotations;

namespace TraineeTrack.API.Models.Requests;

public class SignInRequest
{
    [RequiredEitherUsernameOrEmail]
    [StringLength(30, MinimumLength = 3)]
    public string? Username { get; set; }
    
    [RequiredEitherUsernameOrEmail]
    [StringLength(100, MinimumLength = 3)]
    public string? Email { get; set; }
    
    [Required]
    [Password]
    public required string Password { get; set; }
}