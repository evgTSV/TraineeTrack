using TraineeTrack.API.Models.Requests;
using TraineeTrack.API.Models.Responses;

namespace TraineeTrack.API.Services;

public interface IAuthService
{
    public Task<AuthResponse> SignIn(SignInRequest request);
    public Task<AuthResponse> SignUp(SignUpRequest request);
}