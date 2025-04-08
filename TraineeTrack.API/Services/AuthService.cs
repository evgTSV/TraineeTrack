using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TraineeTrack.API.Exceptions;
using TraineeTrack.API.Models.DB;
using TraineeTrack.API.Models.Requests;
using TraineeTrack.API.Models.Responses;

namespace TraineeTrack.API.Services;

public class AuthService(TraineeTrackContext context, IJwtService jwtService) : IAuthService
{
    private readonly PasswordHasher<string> _passwordHasher = new();
    
    public async Task<AuthResponse> SignIn(SignInRequest request)
    {
        var user = await context.Users.SingleAsync(u => u.Email == (request.Email ?? string.Empty) 
                                                        || u.Username == (request.Username ?? string.Empty));

        if (user is null)
            throw new NotFoundException("User not found");

        var passwordVerify = _passwordHasher.VerifyHashedPassword(user.Email, user.HashedPassword, request.Password);
        
        if (passwordVerify != PasswordVerificationResult.Success)
            throw new ForbiddenException();

        return new AuthResponse { Token = jwtService.GenerateJwtToken(user) };
    }

    public async Task<AuthResponse> SignUp(SignUpRequest request)
    {
        var hashedPassword = _passwordHasher.HashPassword(request.Email, request.Password);
        
        var user = new User
        {
            Email = request.Email,
            Username = request.Username,
            HashedPassword = hashedPassword
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return new AuthResponse { Token = jwtService.GenerateJwtToken(user) };
    }
}