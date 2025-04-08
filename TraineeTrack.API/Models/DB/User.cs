using System.ComponentModel.DataAnnotations;

namespace TraineeTrack.API.Models.DB;

public class User
{
    public Guid Id { get; set; }
    
    [StringLength(30, MinimumLength = 3)]
    public string Username { get; set; } = null!;
    
    [StringLength(100, MinimumLength = 3)]
    public string Email { get; set; } = null!;
    
    [StringLength(1000)]
    public string HashedPassword { get; set; } = null!;
    
    public Uri? AvatarUrl { get; set; }
}