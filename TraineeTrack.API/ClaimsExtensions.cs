using System.Security.Claims;

namespace TraineeTrack.API;

public static class ClaimsExtensions
{
    public static Guid Id(this ClaimsPrincipal principal)
        => Guid.Parse(principal.Identity!.Name!);
}