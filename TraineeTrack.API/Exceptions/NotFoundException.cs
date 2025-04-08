using System.Net;

namespace TraineeTrack.API.Exceptions;

public class NotFoundException(string? reason = null) 
    : HttpException("Not found", HttpStatusCode.NotFound, reason);