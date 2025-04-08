using TraineeTrack.API.Models.DB;
using TraineeTrack.API.Models.Responses;

namespace TraineeTrack.API.Services;

public interface IUserService
{
    public Task<User> GetUserById(Guid userId);
}