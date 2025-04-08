using Microsoft.AspNetCore.Mvc;
using TraineeTrack.API.Models.Requests;
using TraineeTrack.API.Models.Responses;
using TraineeTrack.API.Services;

namespace TraineeTrack.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<AuthResponse> SingUp([FromBody] SignUpRequest request)
        => await authService.SignUp(request);
    
    [HttpPost("sign-in")]
    public async Task<AuthResponse> SingIn([FromBody] SignInRequest request)
        => await authService.SignIn(request);
}