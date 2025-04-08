using Microsoft.IdentityModel.Tokens;
using TraineeTrack.API.Models.DB;

namespace TraineeTrack.API.Services;

public interface IJwtService
{
    public TokenValidationParameters ValidationParameters { get; }
    public string GenerateJwtToken(User user);
    public User this[string token] { get; }
}