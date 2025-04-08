using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeTrack.API.Models.Requests;
using TraineeTrack.API.Models.Responses;
using TraineeTrack.API.Services;

namespace TraineeTrack.API.Controllers;

[Authorize]
[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<UserResponse> Me()
    {
        var user = await userService.GetUserById(User.Id());
        return new UserResponse(user);
    }
}