using System.Net;

namespace TraineeTrack.API.Exceptions;

public class ForbiddenException(string? reason = null)
    : HttpException("Access denied", HttpStatusCode.Forbidden, reason);