using System.Net;
using System.Text;

namespace TraineeTrack.API.Exceptions;

public class HttpException(string message, HttpStatusCode statusCode, string? reason = null) : Exception
{
    public HttpStatusCode StatusCode => statusCode;
    public override string Message => reason is not null
        ? new StringBuilder().Append(message).Append(": ").Append(reason).ToString()
        : message;
}