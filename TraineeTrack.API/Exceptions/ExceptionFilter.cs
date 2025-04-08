using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TraineeTrack.API.Exceptions;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;

        switch (context.Exception)
        {
            case DbUpdateException {
                InnerException: PostgresException {
                    SqlState: PostgresErrorCodes.UniqueViolation
                }
            }:
                context.Result = new ConflictResult();
                break;
            case HttpException httpException:
                context.Result = new ObjectResult(new { Error = httpException.Message }) { StatusCode = (int)httpException.StatusCode };
                break;
            default:
                context.ExceptionHandled = false;
                break;
        }
    }
}