using System.Diagnostics.CodeAnalysis;
using TraineeTrack.API.Models.DB;

namespace TraineeTrack.API.Models.Responses;

public class UserResponse
{
    public UserResponse() {}
    
    [SetsRequiredMembers]
    public UserResponse(User dbUser)
    {
        Username = dbUser.Username;
        Email = dbUser.Email;
        AvatarUrl = dbUser.AvatarUrl;
    }
    
    public required string Username { get; set; }
    public required string Email { get; set; }
    public Uri? AvatarUrl { get; set; }
}