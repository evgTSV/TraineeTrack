using TraineeTrack.API.Exceptions;
using TraineeTrack.API.Models.DB;
using TraineeTrack.API.Models.Responses;

namespace TraineeTrack.API.Services;

public class UserService(TraineeTrackContext context) : IUserService
{
    public async Task<User> GetUserById(Guid userId)
    {
        var user = await context.Users.FindAsync(userId);

        if (user == null)
            throw new NotFoundException("User not found");

        return user;
    }
}